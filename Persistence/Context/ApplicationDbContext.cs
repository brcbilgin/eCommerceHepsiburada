using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.Context
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private IDbContextTransaction _currentTransaction;
        private ILogger _logger;
        private ISystemTimeService _systemTimeService;
        public bool HasActiveTransaction => _currentTransaction != null;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ILogger<ApplicationDbContext> logger, ISystemTimeService systemTimeService)
            : base(options)
        {
            _logger = logger;
            _systemTimeService = systemTimeService;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().Property(p => p.Price).HasColumnType("decimal(10, 2)");
            modelBuilder.Entity<Order>().Property(p => p.Discount).HasColumnType("decimal(10, 2)");
            modelBuilder.Entity<Order>().Property(p => p.UnitPrice).HasColumnType("decimal(10, 2)");
            modelBuilder.Entity<Order>().Property(p => p.TotalPrice).HasColumnType("decimal(10, 2)");
            modelBuilder.Entity<Campaign>().Property(p => p.DiscountPercent).HasColumnType("decimal(10, 2)");
            modelBuilder.Entity<Campaign>().Property(p => p.PriceManipulationLimit).HasColumnType("decimal(10, 2)");
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }

     
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (HasActiveTransaction)
                throw new Exception($"There is already transaction started. Transaction Id : {_currentTransaction.TransactionId}");

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return _currentTransaction;
        }

        public async Task CommitTransactionAsync()
        {
            if (!this.HasActiveTransaction)
                throw new ArgumentNullException($"there is no active transaction for CommitTransactionAsync()");

            try
            {
                await SaveChangesAsync();
                await _currentTransaction.CommitAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Transaction CommitTransactionAsync has error: {ex.Message}");
                await RollbackTransactionASync();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
        public async Task RollbackTransactionASync()
        {
            try
            {
                if (HasActiveTransaction)
                    await _currentTransaction.RollbackAsync();
                else
                    _logger.LogError($"Transaction is null for rollback");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Transaction Rollback has error: {ex.Message}");
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
        public void RollbackTransaction()
        {
            try
            {
                if (this.HasActiveTransaction)
                    _currentTransaction.Rollback();
                else
                    _logger.LogError($"Transaction is null for rollback");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Transaction Rollback has error: {ex.Message}");
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
        public async Task<int> SaveChangesAsync()
        {
            SetBaseProperties();
            return await base.SaveChangesAsync();
        }
        private void SetBaseProperties()
        {
           var changedEntries = ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged);
            Parallel.ForEach(changedEntries, (changedEntry) =>
            {
                var entity = changedEntry.Entity as BaseEntity;

                if (changedEntry.State == EntityState.Added)
                {
                    entity.CreatedDate = _systemTimeService.SystemTime;
                }
                else if (changedEntry.State == EntityState.Modified)
                {
                    entity.ModifiedDate = _systemTimeService.SystemTime;
                }
            });
        }
    }
}

using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Persistence
{
    public class TransactionalProcess : ITransactionalProcess
    {
        private readonly ILogger<TransactionalProcess> _logger;
        private readonly IApplicationDbContext _context;
        private IDbContextTransaction _dbTransaction;

        public TransactionalProcess(ILogger<TransactionalProcess> logger, IApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            string actionName = ((ControllerBase)context.Controller).ControllerContext.ActionDescriptor.DisplayName;

            _dbTransaction = await _context.BeginTransactionAsync();

            _logger.LogInformation($"***** Begin transaction. TransactionId: {_dbTransaction.TransactionId} for {actionName}");

            var result = await next();

            if (IsValidResult(result))
            {
                await _context.CommitTransactionAsync();
                _logger.LogInformation($"***** Commit transaction {_dbTransaction?.TransactionId} for {actionName}");
            }
            else
            {
                _context.RollbackTransaction();
                _logger.LogInformation($"***** RollBack transaction {_dbTransaction.TransactionId} for {actionName}");
            }
        }

        private bool IsValidResult(ActionExecutedContext result)
        {
            return result.Exception == null &&
                   result.Result != null;
        }
    }
}

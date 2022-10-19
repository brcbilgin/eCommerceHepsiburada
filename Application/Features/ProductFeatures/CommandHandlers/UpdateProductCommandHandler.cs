using Application.Exception;
using Application.Features.ProductFeatures.CommandHandlers.Commands;
using Application.Features.ProductFeatures.CommandHandlers.Results;
using Application.Interfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ProductFeatures.CommandHandlers
{

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, UpdateProductResult>
    {
        private readonly IApplicationDbContext _context;
        public UpdateProductCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = _context.Products.Where(p => p.ProductCode == command.ProductCode).FirstOrDefault();
            if (product == null)
            {
                throw new ValidationException("Ürün bulunamadı!");
            }

            product.Stock = command.Stock;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return new UpdateProductResult();
        }
    }
}

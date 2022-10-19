using Application.Dto;
using Application.Features.ProductFeatures.CommandHandlers.Commands;
using Application.Features.ProductFeatures.CommandHandlers.Results;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ProductFeatures.CommandHandlers
{

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            ProductDto productDto = new ProductDto
            {
                ProductCode = command.ProductCode,
                Price = command.Price,
                Stock = command.Stock
            };

            var product = _mapper.Map<Product>(productDto);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return new CreateProductResult
            {
                ProductCode = product.ProductCode,
                Price = product.Price,
                Stock = product.Stock
            };
        }
    }
}

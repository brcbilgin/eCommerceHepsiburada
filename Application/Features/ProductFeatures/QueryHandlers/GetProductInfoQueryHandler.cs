using Application.Dto;
using Application.Features.ProductFeatures.QueryHandlers.Queries;
using Application.Features.ProductFeatures.QueryHandlers.Results;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ProductFeatures.QueryHandlers
{
    public class GetProductInfoQueryHandler : IRequestHandler<GetProductInfoQuery, GetProductInfoResult>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetProductInfoQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<GetProductInfoResult> Handle(GetProductInfoQuery query, CancellationToken cancellationToken)
        {
            var product = _context.Products.Where(a => a.ProductCode == query.ProductCode).FirstOrDefault();
            if (product == null) 
                return null;

            var productDto = _mapper.Map<ProductDto>(product);
            return await Task.FromResult(new GetProductInfoResult
            {
                ProductCode = productDto.ProductCode,
                Price = productDto.Price,
                Stock = productDto.Stock
            });
        }
    }
}

using Application.Dto;
using Application.Exception;
using Application.Features.OrderFeatures.CommandHandlers.Commands;
using Application.Features.OrderFeatures.CommandHandlers.Results;
using Application.Features.ProductFeatures.CommandHandlers.Commands;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.OrderFeatures.CommandHandlers
{

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreateOrderResult>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CreateOrderCommandHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var product = _context.Products.Where(x => x.ProductCode == command.ProductCode).FirstOrDefault();
            if (product == null)
            {
                throw new ValidationException("Ürün kaydı bulunamadı!");
            }
            var totalProductSale = _context.Orders.Where(x => x.ProductCode == command.ProductCode).Sum(a => a.Quantity);
            int currentStock = product.Stock - (totalProductSale + command.Quantity);
            bool hasStock = currentStock > 0;
            if (!hasStock)
            {
                throw new ValidationException($"Stok yetersiz! Mevcut stok miktarı: {currentStock}");
            }

            OrderDto orderDto = new OrderDto();
            orderDto.ProductCode = command.ProductCode;
            orderDto.UnitPrice = product.Price;
            orderDto.Quantity = command.Quantity;
            orderDto.TotalPrice = product.Price * command.Quantity;
            SetOrderCampaingInfo(orderDto);

            var order = _mapper.Map<Order>(orderDto);
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            await _mediator.Send(new UpdateProductCommand { ProductCode = command.ProductCode, Stock = product.Stock - order.Quantity });

            return new CreateOrderResult
            {
                ProductCode = order.ProductCode,
                Quantity = order.Quantity
            };
        }

     
        private void SetOrderCampaingInfo(OrderDto orderDto)
        {
            var campaign = _context.Campaigns.Where(x => x.ProductCode == orderDto.ProductCode && x.IsActive).FirstOrDefault();
            if (campaign != null)
            {
                decimal baseAmount = orderDto.UnitPrice * orderDto.Quantity;
                decimal discount = baseAmount * campaign.DiscountPercent / 100;

                orderDto.TotalPrice = baseAmount - discount;
                orderDto.Discount = discount;
                orderDto.CampaignId = campaign.Id;
            }
        }
    }
}

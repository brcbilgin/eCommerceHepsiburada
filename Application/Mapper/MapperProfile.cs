using Application.Dto;
using AutoMapper;
using Domain.Entities;

namespace WebApi
{
    public class MapperProfile : Profile
    {

        public MapperProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<Order, OrderDto>();
            CreateMap<Campaign, CampaignDto>();
            CreateMap<ProductDto, Product>();
            CreateMap<OrderDto, Order>();
            CreateMap<CampaignDto, Campaign>();
        }
    }
}

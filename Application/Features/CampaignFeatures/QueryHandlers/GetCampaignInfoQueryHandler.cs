using Application.Dto;
using Application.Features.CampaignFeatures.QueryHandlers.Queries;
using Application.Features.CampaignFeatures.QueryHandlers.Results;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.CampaignFeatures.QueryHandlers
{
    public class GetCampaignInfoQueryHandler : IRequestHandler<GetCampaignInfoQuery, GetCampaignInfoResult>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCampaignInfoQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<GetCampaignInfoResult> Handle(GetCampaignInfoQuery query, CancellationToken cancellationToken)
        {
            var campaign = _context.Campaigns.Where(a => a.Name == query.Name).FirstOrDefault();
            if (campaign == null)
                return null;

            var sumOrderValues = _context.Orders.Where(a => a.CampaignId == campaign.Id)
                 .GroupBy(a => new { a.Quantity, a.TotalPrice })
                 .Select(x => new
                 {
                     SumQuantity = x.Sum(y => y.Quantity),
                     SumTotalPrice = x.Sum(y => y.TotalPrice)
                 }).FirstOrDefault();

            var campaignDto = _mapper.Map<CampaignDto>(campaign);

            if (sumOrderValues != null && sumOrderValues.SumQuantity != 0)
            {
                campaignDto.AverageItemPrice = sumOrderValues.SumTotalPrice / sumOrderValues.SumQuantity;
                campaignDto.TotalSales = sumOrderValues.SumQuantity;
                campaignDto.Turnover = campaignDto.TotalSales * campaignDto.AverageItemPrice;
            }

            return await Task.FromResult(new GetCampaignInfoResult
            {
                Name = campaignDto.Name,
                Status = campaignDto.Status,
                TargetSalesCount = campaignDto.TargetSalesCount,
                TotalSales = campaignDto.TotalSales,
                Turnover = campaignDto.Turnover,
                AverageItemPrice = campaignDto.AverageItemPrice
            });
        }
    }
}

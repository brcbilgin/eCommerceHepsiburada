using Application.Dto;
using Application.Features.CampaignFeatures.CommandHandlers.Commands;
using Application.Features.CampaignFeatures.CommandHandlers.Results;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.CampaignFeatures.CommandHandlers
{

    public class CreateCampaignCommandHandler : IRequestHandler<CreateCampaignCommand, CreateCampaignResult>
    {
        private readonly IApplicationDbContext _context;
        private readonly ISystemTimeService _systemTimeService;
        private readonly IMapper _mapper;
        public CreateCampaignCommandHandler(IApplicationDbContext context, ISystemTimeService systemTimeService, IMapper mapper)
        {
            _context = context;
            _systemTimeService = systemTimeService;
            _mapper = mapper;
        }
        public async Task<CreateCampaignResult> Handle(CreateCampaignCommand command, CancellationToken cancellationToken)
        {
            CampaignDto campaignDto = new CampaignDto
            {
                Name = command.Name,
                ProductCode = command.ProductCode,
                Duration = command.Duration,
                PriceManipulationLimit = command.PriceManipulationLimit,
                TargetSalesCount = command.TargetSalesCount,
                BeginDate = _systemTimeService.SystemTime,
                IsActive = true
            };

            var campaign = _mapper.Map<Campaign>(campaignDto);
            _context.Campaigns.Add(campaign);
            await _context.SaveChangesAsync();

            return new CreateCampaignResult
            {                
                Name = campaign.Name,
                ProductCode = campaign.ProductCode,
                Duration = campaign.Duration,
                PriceManipulationLimit = campaign.PriceManipulationLimit,
                TargetSalesCount = campaign.TargetSalesCount
            };
        }
    }
}

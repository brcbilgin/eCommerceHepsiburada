using Application.Features.CampaignFeatures.CommandHandlers.Commands;
using Application.Features.CampaignFeatures.CommandHandlers.Results;
using Application.Interfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.CampaignFeatures.CommandHandlers
{

    public class UpdateAllCampaignsStatusCommandHandler : IRequestHandler<UpdateAllCampaignsStatusCommand, UpdateAllCampaignsStatusResult>
    {
        private readonly IApplicationDbContext _context;
        private readonly ISystemTimeService _systemTimeService;
        public UpdateAllCampaignsStatusCommandHandler(IApplicationDbContext context, ISystemTimeService systemTimeService)
        {
            _context = context;
            _systemTimeService = systemTimeService;
        }
        public async Task<UpdateAllCampaignsStatusResult> Handle(UpdateAllCampaignsStatusCommand command, CancellationToken cancellationToken)
        {
           var passiveCampaigns = _context.Campaigns.Where(a => a.IsActive && a.BeginDate.Hour + a.Duration < _systemTimeService.SystemTime.Hour).ToList();
            passiveCampaigns.ForEach(a => a.IsActive = false);
            await _context.SaveChangesAsync();

            return new UpdateAllCampaignsStatusResult();
        }
    }
}

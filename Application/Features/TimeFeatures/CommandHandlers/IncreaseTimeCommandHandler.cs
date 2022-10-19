using Application.Features.CampaignFeatures.CommandHandlers.Commands;
using Application.Features.TimeFeatures.CommandHandlers.Commands;
using Application.Features.TimeFeatures.CommandHandlers.Results;
using Application.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.TimeFeatures.CommandHandlers
{

    public class IncreaseTimeCommandHandler : IRequestHandler<IncreaseTimeCommand, IncreaseTimeResult>
    {
        private readonly IMediator _mediator;
        private readonly ISystemTimeService _systemTimeService;

        public IncreaseTimeCommandHandler(IMediator mediator, ISystemTimeService systemTimeService)
        {
            _mediator = mediator;
            _systemTimeService = systemTimeService;
        }
        public async Task<IncreaseTimeResult> Handle(IncreaseTimeCommand command, CancellationToken cancellationToken)
        {
           string time = _systemTimeService.IncreaseTime(command.Hour);
           await _mediator.Send(new UpdateAllCampaignsStatusCommand());
           await _mediator.Send(new UpdateCampaignDiscountCommand());

            return new IncreaseTimeResult
            {
                Time = time
            };
        }
       
    }
}

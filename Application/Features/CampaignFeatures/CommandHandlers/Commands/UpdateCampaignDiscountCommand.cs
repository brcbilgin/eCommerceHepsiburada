using Application.Features.CampaignFeatures.CommandHandlers.Results;
using MediatR;

namespace Application.Features.CampaignFeatures.CommandHandlers.Commands
{
    public class UpdateCampaignDiscountCommand : RequestBase, IRequest<UpdateCampaignDiscountResult>
    {

    }
}

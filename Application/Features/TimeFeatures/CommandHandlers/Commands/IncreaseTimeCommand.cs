using Application.Features.TimeFeatures.CommandHandlers.Results;
using MediatR;

namespace Application.Features.TimeFeatures.CommandHandlers.Commands
{
    public class IncreaseTimeCommand : RequestBase, IRequest<IncreaseTimeResult>
    {
        public int Hour { get; set; }
    }
}

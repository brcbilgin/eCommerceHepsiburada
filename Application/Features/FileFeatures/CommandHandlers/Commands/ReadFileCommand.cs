using Application.Features.FileFeatures.CommandHandlers.Results;
using MediatR;

namespace Application.Features.FileFeatures.CommandHandlers.Commands
{
    public class ReadFileCommand : RequestBase, IRequest<ReadFileResult>
    {
        public string FilePath { get; set; }
    }
}

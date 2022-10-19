using Application.Features.CampaignFeatures.CommandHandlers.Commands;
using Application.Features.CampaignFeatures.QueryHandlers.Queries;
using Application.Features.FileFeatures.CommandHandlers.Commands;
using Application.Features.FileFeatures.CommandHandlers.Results;
using Application.Features.OrderFeatures.CommandHandlers.Commands;
using Application.Features.ProductFeatures.CommandHandlers.Commands;
using Application.Features.ProductFeatures.QueryHandlers.Queries;
using Application.Features.TimeFeatures.CommandHandlers.Commands;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.TimeFeatures.CommandHandlers
{

    public class ReadFileCommandHandler : IRequestHandler<ReadFileCommand, ReadFileResult>
    {
        private IMediator _mediator;
        public ReadFileCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<ReadFileResult> Handle(ReadFileCommand command, CancellationToken cancellationToken)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string line in System.IO.File.ReadLines(command.FilePath, Encoding.GetEncoding("UTF-8")))
            {
                var commandArray = line.Split(' ');

                string cmdName = commandArray[0];

                switch (cmdName)
                {
                    case "create_product":
                        var createProductResult = await _mediator.Send(new CreateProductCommand { ProductCode = commandArray[1], Price = Convert.ToDecimal(commandArray[2]), Stock = Convert.ToInt32(commandArray[3]) });
                        sb.Append("Product created.");
                        sb.Append(JsonConvert.SerializeObject(createProductResult));
                        break;

                    case "get_product_info":
                        var getProductInfoResult = await _mediator.Send(new GetProductInfoQuery { ProductCode = commandArray[1] });
                        sb.Append($"Product { commandArray[1] } info");
                        sb.Append(JsonConvert.SerializeObject(getProductInfoResult));
                        break;

                    case "create_order":
                        var createOrderResult = await _mediator.Send(new CreateOrderCommand { ProductCode = commandArray[1], Quantity = Convert.ToInt32(commandArray[2]) });
                        sb.Append("Order created.");
                        sb.Append(JsonConvert.SerializeObject(createOrderResult));
                        break;

                    case "create_campaign":
                        var createCampaignResult = await _mediator.Send(new CreateCampaignCommand { Name = commandArray[1], ProductCode = commandArray[2], Duration = Convert.ToInt32(commandArray[3]), PriceManipulationLimit = Convert.ToDecimal(commandArray[4]), TargetSalesCount = Convert.ToInt32(commandArray[5]) });
                        sb.Append("Campaign created.");
                        sb.Append(JsonConvert.SerializeObject(createCampaignResult));
                        break;

                    case "get_campaign_info":
                        var getCampaignInfoResult = await _mediator.Send(new GetCampaignInfoQuery { Name = commandArray[1] });
                        sb.Append($"Campaign { commandArray[1] } info");
                        sb.Append(JsonConvert.SerializeObject(getCampaignInfoResult));
                        break;

                    case "increase_time":
                        var increaseTimeResult = await _mediator.Send(new IncreaseTimeCommand { Hour = Convert.ToInt32(commandArray[1]) });
                        sb.Append("Time is:");
                        sb.Append(JsonConvert.SerializeObject(increaseTimeResult));
                        break;

                    default:
                        break;
                }
            }

            return new ReadFileResult { CommandsResults = sb.ToString() };
        }

    }
}

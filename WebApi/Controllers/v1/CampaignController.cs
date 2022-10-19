using Application.Features.CampaignFeatures.CommandHandlers.Commands;
using Application.Features.CampaignFeatures.QueryHandlers.Queries;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using System.Threading.Tasks;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class CampaignController : BaseApiController
    {
        /// <summary>
        /// Creates Campaign
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ServiceFilter(typeof(TransactionalProcess))]
        public async Task<IActionResult> CreateCampaign(CreateCampaignCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Gets Campaign info by Name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{name}")]
        public async Task<IActionResult> GetCampaignInfo(string name)
        {
            return Ok(await Mediator.Send(new GetCampaignInfoQuery { Name = name }));
        }       
    }
}
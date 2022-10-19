using Application.Features.TimeFeatures.CommandHandlers.Commands;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class TimeController : BaseApiController
    {
        /// <summary>
        /// Increases time
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> IncreaseTime(IncreaseTimeCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
using System.Threading.Tasks;
using Application.Features.OrderFeatures.CommandHandlers.Commands;
using Microsoft.AspNetCore.Mvc;
using Persistence;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class OrderController : BaseApiController
    {
        /// <summary>
        /// Creates Order
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ServiceFilter(typeof(TransactionalProcess))]
        public async Task<IActionResult> CreateOrder(CreateOrderCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
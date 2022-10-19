using Application.Features.ProductFeatures.CommandHandlers.Commands;
using Application.Features.ProductFeatures.QueryHandlers.Queries;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using System.Threading.Tasks;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ProductController : BaseApiController
    {
        /// <summary>
        /// Creates Product
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ServiceFilter(typeof(TransactionalProcess))]
        public async Task<IActionResult> CreateProduct(CreateProductCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Gets Product info by ProductCode.
        /// </summary>
        /// <param name="productCode"></param>
        /// <returns></returns>
        [HttpGet("{productCode}")]
        public async Task<IActionResult> GetProductInfo(string productCode)
        {
            return Ok(await Mediator.Send(new GetProductInfoQuery { ProductCode = productCode }));
        }       
    }
}
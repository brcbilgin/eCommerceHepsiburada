using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITransactionalProcess : IAsyncActionFilter
    {
        Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next);
    }
}

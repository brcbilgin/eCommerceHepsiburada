using Application.Features.FileFeatures.CommandHandlers.Commands;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class FileController : BaseApiController
    {
        private IConfiguration _config;
        private IWebHostEnvironment _env;
        public FileController(IConfiguration config, IWebHostEnvironment env)
        {
            _config = config;
            _env = env; 
        }

        /// <summary>
        /// Reads File From appsetting.json
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ReadFile()
        {            
            var directory = System.IO.Directory.GetParent(_env.ContentRootPath).FullName; 
            string filePath = directory + _config.GetValue<string>("ReadFilePath");
            return Ok(await Mediator.Send(new ReadFileCommand { FilePath = filePath }));
        }
    }
}
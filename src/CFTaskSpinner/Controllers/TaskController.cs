using CFTaskSpinner.Filter;
using CFTaskSpinner.MessagePacket;
using CFTaskSpinner.Service;
using CloudFoundry.CloudController.V3.Client.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CFTaskSpinner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(LoginAttribute))]
    public class TaskController : ControllerBase
    {
        private ILogger<TaskController> _logger;
        private readonly ICloudFoundryServices _cfService;
        public TaskController(ILogger<TaskController> logger, ICloudFoundryServices cfService)
        {
            _logger = logger;
            _cfService = cfService;
        }
        [HttpPost("Create")]
        public async Task<ActionResult>Create(ReqTaskInfo reqTaskInfo)
        {
            _logger.LogInformation("Task Controller Create...");
            var appId = await _cfService.GetAppIdAsync(reqTaskInfo.ApplicationName);
            var responseContent = await _cfService.CreateTaskAsync(appId, reqTaskInfo?.TaskName,
                reqTaskInfo?.StartCommand, reqTaskInfo?.CommandArguments);
            return Ok(responseContent);
        }
        [HttpPost("Cancel")]
        public async Task<IActionResult>Cancel(Guid taskGuid)
        {
            _logger.LogInformation("Task Controller Cancel...");
            var responseContent = await _cfService.CancelTaskAsync(taskGuid);
            return Ok(responseContent);
        }
        [HttpGet("Get")]
        public async Task<IActionResult>Get(Guid taskGuid)
        {
            _logger.LogInformation("Task Controller Get...");
            var response = await _cfService.GetTaskAsync(taskGuid);
            return Ok(response);
        }
        [HttpGet("GetAllByApp")]
        public async Task<IActionResult>GetAllByApp(Guid appGuid, string stateCsv)
        {
            _logger.LogInformation("Task Controller GetAllByApp...");
            var response = await _cfService.ListAppTaskAsync(appGuid, stateCsv);
            if ((response.resources != null) && (response.resources.Any()))
                return Ok(new List<Resource>(response.resources));
            else
                return Ok(new List<Resource>());
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult>GetAll(string stateCsv)
        {
            _logger.LogInformation("Task Controller GetAll...");
            var response = await _cfService.ListTaskAsync(stateCsv);
            if ((response.resources != null) && (response.resources.Any()))
                return Ok(new List<Resource>(response.resources));
            else
                return Ok(new List<Resource>());
        }
        [HttpGet]
        public IActionResult Index()
        {
            _logger.LogInformation("Task Controller Running...");
            return Ok("Task Controller Running....");
        }
    }
}

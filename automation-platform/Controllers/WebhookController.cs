using Microsoft.AspNetCore.Mvc;
using automation_platform.Dtos;
using automation_platform.Services;
using automation_platform.Models;

namespace automation_platform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WebhookController : ControllerBase
    {
        private readonly ILogger<WebhookController> logger;
        private readonly IWorkflowService service;
        public WebhookController(ILogger<WebhookController> logger, IWorkflowService service) 
        { 
            this.logger = logger;
            this.service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddWebhook(WebhookDto dto)
        {            
            var request = await service.ExecuteWebhookWorkflow();
            logger.LogInformation($"Webhook: \n Id: {dto.Id},\n Name: {dto.Name},\n Description: {dto.Description}");

            if (request is true)
                return Ok();
            
            return StatusCode(500);
        }

    }
}

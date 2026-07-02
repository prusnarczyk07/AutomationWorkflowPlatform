using Microsoft.AspNetCore.Mvc;
using automation_platform.Dtos;
using automation_platform.Services;

namespace automation_platform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WebhookController : ControllerBase
    {
        private readonly ILogger<WebhookController> logger;
        private readonly IDiscordService service;
        public WebhookController(ILogger<WebhookController> logger, IDiscordService service) 
        { 
            this.logger = logger;
            this.service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddWebhook(WebhookDto dto)
        {
            var request = await service.SendDiscordMessage(dto);
            logger.LogInformation($"Webhook: \n Id: {dto.Id},\n Name: {dto.Name},\n Description: {dto.Description}");

            if (request is true)
                return Ok();
            
            return StatusCode(500);
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using automation_platform.Dtos;
using automation_platform.Services;
using automation_platform.Models;
using automation_platform.Repositories;

namespace automation_platform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WebhookController : ControllerBase
    {
        private readonly ILogger<WebhookController> logger;
        private readonly IWorkflowService service;
        public WebhookController(ILogger<WebhookController> logger, IWorkflowService service, IWorkflowRepository repository) 
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

        [HttpPost("workflow")]
        public async Task<IActionResult> AddWorkflow(WorkflowDto dto)
        {            
            await service.CreateWorkflow(dto);

            return Ok();
        }

        [HttpGet("workflows")]
        public async Task<IActionResult> GetWorkflows()
        {
            var workflows = await service.GetWorkflows();

            return Ok(workflows);
        }

        [HttpGet("workflow/{id}")]
        public async Task<IActionResult> GetWorkflowById(int id)
        {
            var workflow = await service.GetWorkflowById(id);

            if (workflow is null)
                return NotFound();

            return Ok(workflow);
        }

        [HttpDelete("workflow/{id}")]
        public async Task<IActionResult> DeleteWorkflowById(int id)
        {
            var deleted = await service.DeleteWorkflowById(id);

            if (deleted is false)
                return NotFound();

            return NoContent();
        }

        [HttpPut("workflow/{id}")]
        public async Task<IActionResult> UpdateWorkflowById(int id, WorkflowDto dto)
        {
            var workflow = new Workflow
            {
                Name = dto.Name,
                Trigger = dto.Trigger,
                Steps = dto.Steps,
            };

            var updated = await service.UpdateWorkflowById(workflow, id);

            if (updated is false)
                return NotFound();

            return NoContent();
        }
    }
}

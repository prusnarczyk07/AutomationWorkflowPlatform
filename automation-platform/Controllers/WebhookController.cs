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
        private readonly IWorkflowService service;
        public WebhookController(ILogger<WebhookController> logger, IWorkflowService service)
        {
            this.service = service;
        }

        [HttpPost("{trigger}")]
        public async Task<IActionResult> AddWebhook(string trigger, [FromBody]WebhookDto dto)
        {            
            var request = await service.ExecuteWebhookWorkflow(trigger, dto);

            if (request is true)
                return Ok();
            
            return StatusCode(500);
        }

        [HttpPost("workflow")]
        public async Task<IActionResult> AddWorkflow(WorkflowDto dto)
        {            
            var created = await service.CreateWorkflow(dto);

            if (created is null)
                return BadRequest("Workflow must contain at least one step.");

            return CreatedAtAction(nameof(GetWorkflowById), new {id = created.Id}, created);
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
            var updated = await service.UpdateWorkflowById(dto, id);

            if (updated is null)
                return NotFound();

            return Ok(updated);
        }
    }
}

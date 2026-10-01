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
            
            return Problem(statusCode: 500, title: "Workflow execution failed", detail: "One of the workflow steps could not be executed");
        }

        [HttpPost("workflow")]
        public async Task<IActionResult> AddWorkflow(WorkflowDto dto)
        {            
            var created = await service.CreateWorkflow(dto);

            if (created.workflow is null)
                return BadRequest(created.error);
            
            return CreatedAtAction(nameof(GetWorkflowById), new {id = created.workflow.Id}, created.workflow);
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

            if (updated.workflow is null)
            {
                if (updated.error == "Workflow not found")
                    return NotFound(updated.error);
                
                return BadRequest(updated.error);
            }
            
            return Ok(updated.workflow);
        }
    }
}

using automation_platform.Dtos;
using automation_platform.Models;

namespace automation_platform.Services
{
    public interface IWorkflowService
    {
        Task<bool> ExecuteWorkflow(Workflow workflow, WebhookDto dto);
        Task<bool> ExecuteWebhookWorkflow(string trigger, WebhookDto dto);
        Task<Workflow?> CreateWorkflow(WorkflowDto dto);
        Task<IEnumerable<Workflow>> GetWorkflows();
        Task<Workflow?> GetWorkflowById(int id);
        Task<bool> DeleteWorkflowById(int id);
        Task<bool> UpdateWorkflowById(WorkflowDto dto, int id);
    }
}

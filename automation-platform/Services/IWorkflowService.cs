using automation_platform.Models;

namespace automation_platform.Services
{
    public interface IWorkflowService
    {
        Task<bool> ExecuteWorkflow(Workflow workflow);
    }
}

using automation_platform.Dtos;
using automation_platform.Models;

namespace automation_platform.Services
{
    public interface IWorkflowStepHandler
    {
        string StepName { get; }
        Task<bool> Execute(WebhookDto dto, WorkflowStep step);
    }
}

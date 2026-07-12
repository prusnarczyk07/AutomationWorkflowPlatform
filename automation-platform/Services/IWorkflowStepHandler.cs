using automation_platform.Dtos;

namespace automation_platform.Services
{
    public interface IWorkflowStepHandler
    {
        string StepName { get; }
        Task<bool> Execute(WebhookDto dto);
    }
}

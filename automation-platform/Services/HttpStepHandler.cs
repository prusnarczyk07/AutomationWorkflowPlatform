using automation_platform.Dtos;

namespace automation_platform.Services
{
    public class HttpStepHandler : IWorkflowStepHandler
    {
        public string StepName => "http";

        public Task<bool> Execute(WebhookDto dto, WorkflowStepDto stepDto)
        {
            Task.FromResult(true);
        }
        
    }
}

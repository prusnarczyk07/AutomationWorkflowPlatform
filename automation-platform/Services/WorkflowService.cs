using automation_platform.Models;
using automation_platform.Dtos;

namespace automation_platform.Services
{
    public class WorkflowService : IWorkflowService
    {
        private readonly IEnumerable<IWorkflowStepHandler> handlers;

        public WorkflowService(IEnumerable<IWorkflowStepHandler> handlers)
        {
            this.handlers = handlers;
        }

        public async Task<bool> ExecuteWorkflow(Workflow workflow)
        {
            var dto = new WebhookDto
            {
                Id = 1,
                Name = "workflow",
                Description = "step executed"
            };

            foreach (string step in workflow.Steps)
            {
                var handler = handlers.FirstOrDefault(x => x.StepName == step);

                if (handler is null)
                    return false;
                
                if (!await handler.Execute(dto))
                    return false;
            }
            return true;
        }
    }
}

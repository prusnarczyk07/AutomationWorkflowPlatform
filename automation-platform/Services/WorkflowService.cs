using automation_platform.Dtos;
using automation_platform.Models;
using automation_platform.Repositories;

namespace automation_platform.Services
{
    public class WorkflowService : IWorkflowService
    {
        private readonly IEnumerable<IWorkflowStepHandler> handlers;
        private readonly IWorkflowRepository repository;

        public WorkflowService(IEnumerable<IWorkflowStepHandler> handlers, IWorkflowRepository repository)
        {
            this.handlers = handlers;
            this.repository = repository;
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

        public async Task<bool> ExecuteWebhookWorkflow()
        {
            var workflow = await repository.GetByTrigger("webhook");

            if (workflow is null)
                return false;

            return await ExecuteWorkflow(workflow);
        }

        public async Task<bool> CreateWorkflow(WorkflowDto dto)
        {
            if (dto.Steps.Count == 0)
                return false;

            var workflow = new Workflow
            {
                Name = dto.Name.Trim(),
                Trigger = dto.Trigger.Trim(),
                Steps = dto.Steps.Select(step => step.Trim()).ToList()
            };

            await repository.Add(workflow);

            return true;
        }

        public Task<IEnumerable<Workflow>> GetWorkflows()
        {
            return repository.GetAll();
        }

        public Task<Workflow?> GetWorkflowById(int id)
        {
            return repository.GetById(id);
        }

        public Task<bool> DeleteWorkflowById(int id)
        {
            return repository.DeleteById(id);
        }

        public Task<bool> UpdateWorkflowById(Workflow workflow, int id)
        {
            return repository.UpdateById(workflow, id);
        }
    }
}

using automation_platform.Dtos;
using automation_platform.Migrations;
using automation_platform.Models;
using automation_platform.Repositories;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace automation_platform.Services
{
    public class WorkflowService : IWorkflowService
    {
        private readonly IEnumerable<IWorkflowStepHandler> handlers;
        private readonly IWorkflowRepository repository;
        private readonly ILogger<WorkflowService> logger;

        public WorkflowService(IEnumerable<IWorkflowStepHandler> handlers, IWorkflowRepository repository, ILogger<WorkflowService> logger)
        {
            this.handlers = handlers;
            this.repository = repository;
            this.logger = logger;
        }

        public async Task<bool> ExecuteWorkflow(Workflow workflow, WebhookDto dto)
        {
            logger.LogInformation("\n\n---Starting workflow {WorkflowId} ({WorkflowName})---\n", workflow.Id, workflow.Name);
            
            foreach (var step in workflow.Steps)
            {
                logger.LogInformation("\t--Executing step {Step} in workflow {WorkflowId}--", step.Type, workflow.Id);

                var handler = handlers.FirstOrDefault(x => x.StepName == step.Type);

                if (handler is null)
                {
                    logger.LogError("No handler found for step {Step} in workflow {WorkflowId}", step.Type, workflow.Id);
                    
                    return false;
                }

                if (!await handler.Execute(dto, step))
                {
                    logger.LogError("Step {Step} failed in workflow {WorkflowId}", step.Type, workflow.Id);

                    return false;
                }
                    
            }

            logger.LogInformation("\n---Workflow {WorkflowId} completed successfully---\n\n", workflow.Id);
            
            return true;
        }

        public async Task<bool> ExecuteWebhookWorkflow(string trigger, WebhookDto dto)
        {
            var workflow = await repository.GetByTrigger(trigger);

            if (workflow is null)
                return false;

            return await ExecuteWorkflow(workflow, dto);
        }

        public async Task<(Workflow? workflow, string? error)> CreateWorkflow(WorkflowDto dto)
        {
            if (dto.Steps.Count == 0)
                return (null, "Workflow must contain at least one step");

            foreach (var step in dto.Steps)
            {
                if (!Uri.TryCreate(step.Url, UriKind.Absolute, out var uri) || (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
                    return (null, "Step URL must be a valid HTTP or HTTPS URL");
            }

            var steps = dto.Steps.Select(step => new WorkflowStep 
            {  
                Type = step.Type.Trim(),
                Url = step.Url.Trim(),
                Method = step.Method.Trim(),
            }).ToList();

            var workflow = new Workflow
            {
                Name = dto.Name.Trim(),
                Trigger = dto.Trigger.Trim(),
                Steps = steps
            };

            await repository.Add(workflow);

            return (workflow, null);
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

        public async Task<(Workflow? workflow, string? error)> UpdateWorkflowById(WorkflowDto dto, int id)
        {
            var workflow = await repository.GetById(id);

            if (workflow is null)
                return (null, "Workflow not found");

            foreach (var step in dto.Steps)
            {
                if (!Uri.TryCreate(step.Url, UriKind.Absolute, out var uri) || uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps)
                    return (null, "Step URL must be a valid HTTP or HTTPS URL");
            }

            var steps = dto.Steps.Select(step => new WorkflowStep
            {
                Type = step.Type.Trim(),
                Url = step.Url.Trim(),
                Method = step.Method.Trim(),
            }).ToList();

            workflow.Name = dto.Name.Trim();
            workflow.Trigger = dto.Trigger.Trim();
            workflow.Steps = steps;

            await repository.UpdateById(workflow, id);
            
            return (workflow, null);
        }
    }
}

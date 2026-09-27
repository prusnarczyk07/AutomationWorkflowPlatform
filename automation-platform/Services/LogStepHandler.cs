using automation_platform.Dtos;
using automation_platform.Models;

namespace automation_platform.Services
{
    public class LogStepHandler : IWorkflowStepHandler
    {
        private readonly ILogger<LogStepHandler> logger;

        public LogStepHandler(ILogger<LogStepHandler> logger)
        {
            this.logger = logger;
        }

        public string StepName => "log";

        public Task<bool> Execute(WebhookDto dto, WorkflowStep step)
        {
            logger.LogInformation("\t--Webhook received: {WebhookName} - {Description} --", dto.Name, dto.Description);

            return Task.FromResult(true);
        }
    }
}

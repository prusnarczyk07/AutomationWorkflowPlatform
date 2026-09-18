using automation_platform.Dtos;

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

        public Task<bool> Execute(WebhookDto dto)
        {
            
            logger.LogInformation("step executed");

            return Task.FromResult(true);
        }
    }
}

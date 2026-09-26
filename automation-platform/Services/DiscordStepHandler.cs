using automation_platform.Dtos;

namespace automation_platform.Services
{
    public class DiscordStepHandler : IWorkflowStepHandler
    {
        private readonly IDiscordService discordService;

        public DiscordStepHandler(IDiscordService discordService, ILogger<DiscordStepHandler> logger)
        {
            this.discordService = discordService;
        }

        public string StepName => "discord";

        public async Task<bool> Execute(WebhookDto dto, WorkflowStepDto stepDto)
        {
            return await discordService.SendDiscordMessage(dto);
        }

    }
}

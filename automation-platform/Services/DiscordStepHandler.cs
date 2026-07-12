using automation_platform.Dtos;

namespace automation_platform.Services
{
    public class DiscordStepHandler : IWorkflowStepHandler
    {
        private readonly IDiscordService discordService;
        public DiscordStepHandler(IDiscordService discordService)
        {
            this.discordService = discordService;
        }

        public string StepName => "discord";

        public async Task<bool> Execute(WebhookDto dto)
        {
            return await discordService.SendDiscordMessage(dto);
        }

    }
}

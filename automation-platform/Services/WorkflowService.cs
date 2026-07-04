using automation_platform.Models;
using automation_platform.Dtos;

namespace automation_platform.Services
{
    public class WorkflowService : IWorkflowService
    {
        private readonly IDiscordService discordService;

        public WorkflowService(IDiscordService discordService)
        {
            this.discordService = discordService;
        }

        public async Task<bool> ExecuteWorkflow(Workflow workflow)
        {
            var dto = new WebhookDto
            {
                Id = 1,
                Name = "workflow",
                Description = "step executed"
            };

            foreach (string step in workflow.Steps){
                if (step == "discord")
                {
                    if (!await discordService.SendDiscordMessage(dto))
                        return false;
                }
                else if (step == "save")
                {

                }
                else if (step == "email")
                {

                }
            }
            return true;
        }
    }
}

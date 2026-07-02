using automation_platform.Dtos;

namespace automation_platform.Services
{
    public interface IDiscordService
    {
        Task<bool> SendDiscordMessage(WebhookDto dto);
    }
}

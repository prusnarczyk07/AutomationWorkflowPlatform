using automation_platform.Dtos;
using automation_platform.Models;

namespace automation_platform.Services
{
    public class DiscordService : IDiscordService
    {
        HttpClient client;
        IConfiguration config;
        string webhookUrl;
        
        public DiscordService(HttpClient client, IConfiguration config) 
        { 
            this.client = client;
            this.config = config;
            webhookUrl = config.GetValue<string>("Discord:WebhookUrl");
        }

        public async Task<bool> SendDiscordMessage(WebhookDto dto)
        {
            var message = $"Webhook received: Id: {dto.Id}, Name: {dto.Name}, Description: {dto.Description}";
            var payload = new
            {
                content = message
            };

            var result = await client.PostAsJsonAsync(webhookUrl, payload);

            return result.IsSuccessStatusCode;
        }
    }
}

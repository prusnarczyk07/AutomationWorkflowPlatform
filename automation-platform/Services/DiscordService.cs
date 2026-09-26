using automation_platform.Dtos;
using automation_platform.Models;

namespace automation_platform.Services
{
    public class DiscordService : IDiscordService
    {
        HttpClient client;
        IConfiguration config;
        string webhookUrl;
        private readonly ILogger<DiscordService> logger;
        
        public DiscordService(HttpClient client, IConfiguration config, ILogger<DiscordService> logger) 
        { 
            this.client = client;
            this.config = config;
            this.logger = logger;
            webhookUrl = config.GetValue<string>("Discord:WebhookUrl") ?? throw new InvalidOperationException("Discord:WebhookUrl is not configured.");
        }

        public async Task<bool> SendDiscordMessage(WebhookDto dto)
        {
            var message = $"Webhook received: Id: {dto.Id}, Name: {dto.Name}, Description: {dto.Description}";
            var payload = new
            {
                content = message
            };

            var result = await client.PostAsJsonAsync(webhookUrl, payload);

            logger.LogInformation("\t--Discord returned HTTP {StatusCode}--", (int)result.StatusCode);

            return result.IsSuccessStatusCode;
        }
    }
}

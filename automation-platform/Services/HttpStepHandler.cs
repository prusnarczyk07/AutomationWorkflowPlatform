using System.Text.Json;
using System.Text;
using automation_platform.Dtos;
using automation_platform.Models;

namespace automation_platform.Services
{
    public class HttpStepHandler : IWorkflowStepHandler
    {
        private readonly HttpClient client;
        private readonly ILogger<HttpStepHandler> logger;

        public HttpStepHandler(HttpClient client, ILogger<HttpStepHandler> logger)
        {
            this.client = client;
            this.logger = logger;
        }

        public string StepName => "http";

        public async Task<bool> Execute(WebhookDto dto, WorkflowStep step)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(step.Url),
                Method = new HttpMethod(step.Method)
            };
            
            logger.LogInformation("\t--Sending {Method} request to {Url}--", step.Method, step.Url);

            string json = JsonSerializer.Serialize(dto);

            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request);

            logger.LogInformation("\t--Http returned {StatusCode}", (int)response.StatusCode);

            return response.IsSuccessStatusCode;
        }
        
    }
}

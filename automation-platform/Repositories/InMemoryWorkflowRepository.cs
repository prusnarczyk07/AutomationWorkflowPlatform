using automation_platform.Models;

namespace automation_platform.Repositories
{
    public class InMemoryWorkflowRepository : IWorkflowRepository
    {
        private readonly List<Workflow> workflows = new()
        {
            new Workflow
            {
                Trigger = "webhook",
                Steps = new List<string> { "discord" }
            }
        };
        
        public Task<Workflow?> GetByTrigger(string trigger)
        {
            var result = workflows.FirstOrDefault(t => t.Trigger == trigger);
            
            return Task.FromResult(result);
        }
    }
}

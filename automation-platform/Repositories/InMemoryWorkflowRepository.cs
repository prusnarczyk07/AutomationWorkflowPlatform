using automation_platform.Models;

namespace automation_platform.Repositories
{
    public class InMemoryWorkflowRepository : IWorkflowRepository
    {
        private readonly List<Workflow> workflows = new()
        {
            new Workflow
            {
                Id = 1,
                Name = "Discord notification",
                Trigger = "webhook",
                Steps = new List<string> { "discord" }
            }
        };

        public Task Add(Workflow workflow)
        {
            workflows.Add(workflow);

            return Task.CompletedTask;
        }
        
        public Task<Workflow?> GetByTrigger(string trigger)
        {
            var result = workflows.FirstOrDefault(t => t.Trigger == trigger);
            
            return Task.FromResult(result);
        }

        public Task<IEnumerable<Workflow>> GetAll()
        {
            return Task.FromResult(workflows.AsEnumerable());
        }

        public Task<Workflow?> GetById(int id)
        {
            return Task.FromResult(workflows.FirstOrDefault(w => w.Id == id));
        }

        public Task<bool> DeleteById(int id)
        {
            var workflow = workflows.FirstOrDefault(w => w.Id == id);

            if (workflow is null)
                return Task.FromResult(false);

            workflows.Remove(workflow);
            
            return Task.FromResult(true);
        }
    }
}

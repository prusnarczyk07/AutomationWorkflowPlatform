using automation_platform.Models;

namespace automation_platform.Repositories
{
    // Temporary repository used for development and testing.
    // Data is stored only in memory and is lost after application restart.
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
        
        public Task<Workflow?> GetByTrigger(string trigger)
        {
            var result = workflows.FirstOrDefault(t => t.Trigger == trigger);
            
            return Task.FromResult(result);
        }

        public Task<Workflow> Add(Workflow workflow)
        {
            workflows.Add(workflow);

            return Task.FromResult(workflow);
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

        public Task<bool> UpdateById(Workflow workflow, int id)
        {
            var foundWorkflow = workflows.FirstOrDefault(w => w.Id == id);
            
            if (foundWorkflow is null)
                return Task.FromResult(false);

            foundWorkflow.Name = workflow.Name;
            foundWorkflow.Trigger = workflow.Trigger;
            foundWorkflow.Steps = workflow.Steps;

            return Task.FromResult(true);
        }
    }
}

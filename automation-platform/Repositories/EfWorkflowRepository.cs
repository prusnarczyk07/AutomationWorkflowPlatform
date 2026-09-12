using automation_platform.Data;
using automation_platform.Models;
using Microsoft.EntityFrameworkCore;

namespace automation_platform.Repositories
{
    public class EfWorkflowRepository : IWorkflowRepository
    {
        private readonly AppDbContext context;

        public EfWorkflowRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<Workflow?> GetByTrigger(string trigger)
        {
            var result = await context.Workflows.FirstOrDefaultAsync(t => t.Trigger == trigger);

            return result;
        }

        public async Task<Workflow> Add(Workflow workflow)
        {
            await context.Workflows.AddAsync(workflow);
            await context.SaveChangesAsync();

            return workflow;
        }

        public async Task<IEnumerable<Workflow>> GetAll()
        {
            var result = await context.Workflows.ToListAsync();

            return result;
        }

        public async Task<Workflow?> GetById(int id)
        {
            var result = await context.Workflows.FindAsync(id);
            
            return result;
        }

        public async Task<bool> DeleteById(int id)
        {
            var workflow = await context.Workflows.FindAsync(id);
            
            if (workflow is null)
                return false;

            context.Workflows.Remove(workflow);

            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateById(Workflow workflow, int id)
        {
            var foundWorkflow = await context.Workflows.FindAsync(id);

            if (foundWorkflow is null)
                return false;

            foundWorkflow.Name = workflow.Name;
            foundWorkflow.Trigger = workflow.Trigger;
            foundWorkflow.Steps = workflow.Steps;

            await context.SaveChangesAsync();
            
            return true;
        }
    }
}

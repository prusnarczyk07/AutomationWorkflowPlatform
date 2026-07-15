using automation_platform.Models;

namespace automation_platform.Repositories
{
    public interface IWorkflowRepository
    {
        Task<Workflow?> GetByTrigger(string trigger);
        Task Add(Workflow workflow);
        Task<IEnumerable<Workflow>> GetAll();
    }
}

using automation_platform.Models;

namespace automation_platform.Repositories
{
    public interface IWorkflowRepository
    {
        Task<Workflow?> GetByTrigger(string trigger);
        Task Add(Workflow workflow);
        Task<IEnumerable<Workflow>> GetAll();
        Task<Workflow?> GetById(int id);
        Task<bool> DeleteById(int id);
        Task<bool> UpdateById(Workflow workflow, int id);
    }
}

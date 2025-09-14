using Data.Models;

namespace DataAccess.Repositories.Interfaces
{
    public interface ITaskRepository : IRepository<TaskModel>
    {
        Task<TaskModel?> GetFullByIdAsync(int id, bool skipTracking = false);
        Task<List<TaskModel>> GetAllAsync(int[]? ids = null, bool skipTracking = false);
        Task<List<TaskModel>> GetFullAllAsync(bool skipTracking = false);
    }

}

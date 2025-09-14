using Data.Models;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class TaskRepository(AppDbContext context) : Repository<TaskModel>(context), ITaskRepository
    {
        public async Task<TaskModel?> GetFullByIdAsync(int id, bool skipTracking = false)
        {
            IQueryable<TaskModel> query = _dbSet;

            query = query
                .Include(t => t.Author)
                .Include(t => t.Performer)
                .Include(t => t.ParentTask)
                .Include(t => t.Subtasks);

            if (skipTracking)
                query.AsNoTracking();

            return await query.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<TaskModel>> GetAllAsync(int[]? ids = null, bool skipTracking = false)
        {
            IQueryable<TaskModel> query = _dbSet;

            if (ids != null)
                query = _dbSet.Where(t => ids.Contains(t.Id));

            if (skipTracking)
                query.AsNoTracking();

            return await query.ToListAsync();
        }

        public async Task<List<TaskModel>> GetFullAllAsync(bool skipTracking = false)
        {
            IQueryable<TaskModel> query = _dbSet;

            query = query
                .Include(t => t.Author)
                .Include(t => t.Performer)
                .Include(t => t.ParentTask)
                .Include(t => t.Subtasks);

            if (skipTracking)
                query.AsNoTracking();

            return await query.ToListAsync();
        }
    }
}

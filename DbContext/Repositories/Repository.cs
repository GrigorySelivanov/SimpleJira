using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public abstract class Repository<T>(AppDbContext context) : IRepository<T> where T : class
    {
        protected internal readonly AppDbContext _dbContext = context;
        protected internal DbSet<T> _dbSet = context.Set<T>();

        public virtual async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
        public virtual async Task CreateAsync(T entity) => await _dbSet.AddAsync(entity);
        public virtual void Update(T entity) => _dbSet.Update(entity);
        public virtual void Remove(T entity) => _dbSet.Remove(entity);
        public virtual void RemoveRange(IEnumerable<T> entities) => _dbSet.RemoveRange(entities);
        public virtual async Task SaveAsync() => await  _dbContext.SaveChangesAsync();
    }
}

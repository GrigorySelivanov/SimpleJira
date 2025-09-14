namespace DataAccess.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        Task<T?> GetByIdAsync(int id);
        Task CreateAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        Task SaveAsync();
    }
}

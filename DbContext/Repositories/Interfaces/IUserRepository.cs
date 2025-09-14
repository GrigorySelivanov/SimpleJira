using Data.Models;

namespace DataAccess.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByLoginAsync(string login);
    }
}

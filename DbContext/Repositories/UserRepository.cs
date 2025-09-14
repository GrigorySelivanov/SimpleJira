using Data.Models;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class UserRepository(AppDbContext context) : Repository<User>(context), IUserRepository
    {
        public async Task<User?> GetByLoginAsync(string login) =>
            await _dbSet.AsNoTracking().FirstOrDefaultAsync(u => u.Login == login);
    }
}

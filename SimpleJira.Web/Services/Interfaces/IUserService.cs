using Data.DTOs;
using Data.Models;

namespace SimpleJira.Web.Services.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetUserByIDAsync(int id);
    }
}

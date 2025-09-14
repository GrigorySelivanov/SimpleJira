using Data.Models;
using DataAccess.Repositories.Interfaces;
using DataAccess.Repositories;
using SimpleJira.Web.Services.Interfaces;
using SimpleJira.Web.Controllers;

namespace SimpleJira.Web.Services
{
    public class UserService(IUserRepository userRepository, ILogger<UserService> logger) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ILogger<UserService> _logger = logger;
        
        /// <summary>
        /// Метод для получения пользователя по ID.
        /// </summary>
        /// <param name="id">ID пользователя.</param>
        /// <returns>Пользователь или null, если он не найден.</returns>
        public async Task<User?> GetUserByIDAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                _logger.LogWarning("Пользователь с id ={} не найден в базе", id);
            }

            return user;
        }
    }
}

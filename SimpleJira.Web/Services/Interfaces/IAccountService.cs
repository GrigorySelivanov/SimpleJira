using Data.DTOs.UserDTOs;
using Data.Models;

namespace SimpleJira.Web.Services.Interfaces
{
    public interface IAccountService
    {
        Task<LoginResponseDTO> LoginUserAsync(LoginRequestDTO request);
    }
}

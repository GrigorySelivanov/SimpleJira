using AutoMapper;
using Data.DTOs.UserDTOs;
using Data.Models;
using DataAccess.Helpers;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using SimpleJira.Web.Services.Interfaces;

namespace SimpleJira.Web.Services
{
    public class AccountService(
        IUserRepository userRepository,
        ITokenService tokenService,
        IMapper mapper) : IAccountService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ITokenService _tokenService = tokenService;
        private readonly IMapper _mapper = mapper;


        public async Task<LoginResponseDTO> LoginUserAsync(LoginRequestDTO request)
        {
            var user = await _userRepository.GetByLoginAsync(request.Login)
                ?? throw new KeyNotFoundException("Некорректные данные логина или пароля.");

            if (CryptographyHelper.HashPassword(request.Password) != user.PasswordHash)
                throw new KeyNotFoundException("Некорректные данные логина или пароля.");

            var accessToken = _tokenService.CreateToken(user);

            var response = _mapper.Map<LoginResponseDTO>(user);
            response.Token = accessToken;

            return response;
        }
    }
}

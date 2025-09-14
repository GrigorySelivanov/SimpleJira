using Data.DTOs.UserDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleJira.Web.Services.Interfaces;

namespace SimpleJira.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController(
    IAccountService accountService,
    ILogger<AccountController> logger
    ) : ControllerBase
    {
        private readonly IAccountService _accountService = accountService;
        private readonly ILogger<AccountController> _logger = logger;


        /// <summary>
        /// Аутентификация пользователя для дальнейшего доступа к другим ресурсам.
        /// </summary>
        /// <param name="model">Модель запроса.</param>
        /// <returns>Код ответа + DTO пользователя с токеном.</returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDTO>> Login([FromBody] LoginRequestDTO model)
        {
            try
            {
                var response = await _accountService.LoginUserAsync(model);
                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning("{}", ex.Message);
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ошибка генерации токена: {}", ex.Message);
                return BadRequest("Возникла ошибка при входе, обратитесь к администратору.");
            }

        }

    }
}

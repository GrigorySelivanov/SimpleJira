using Data.Models;
using Microsoft.IdentityModel.Tokens;
using SimpleJira.Web.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SimpleJira.Web.Services;

public class TokenService(IConfiguration _configuration) : ITokenService
{
    private readonly IConfiguration _configuration = _configuration;

    /// <summary>
    /// Метод созданиет временный JWT-токен по настройкам, прописанным в <see href="appsettings.json">appsettings.json</see>
    /// </summary>
    /// <param name="user">Пользователь</param>
    /// <returns>Готовый токен</returns>
    public string CreateToken(User user)
    {
        var claims = new List<Claim>()
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Login!)
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Publisher"],
            audience: _configuration["JwtSettings:Consumer"],
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(_configuration.GetSection("JwtSettings:ExpiryMinutes").Get<int>())),
            signingCredentials:
                new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]!)),
                    SecurityAlgorithms.HmacSha256)
                ); ;

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
using Data.Models;

namespace SimpleJira.Web.Services.Interfaces;

public interface ITokenService
{
    string CreateToken(User user);
}
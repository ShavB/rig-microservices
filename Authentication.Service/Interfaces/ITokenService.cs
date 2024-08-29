using Authentication.Service.Models;

namespace Authentication.Service.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);
}
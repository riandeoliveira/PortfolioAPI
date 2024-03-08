using System.IdentityModel.Tokens.Jwt;

using Portfolio.Domain.Entities;

namespace Portfolio.Utils.Interfaces;

public interface IAuthService
{
    string GenerateToken(User user);
    Guid GetLoggedInUserId();
    string? GetToken();
}

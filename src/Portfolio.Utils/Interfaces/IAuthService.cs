using Portfolio.Entities;

namespace Portfolio.Utils.Interfaces;

public interface IAuthService
{
    string GenerateToken(User user);

    string GetToken();
}
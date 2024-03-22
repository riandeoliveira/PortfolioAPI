using Portfolio.Domain.Entities;

namespace Portfolio.Domain.Interfaces;

public interface IAuthService
{
    string GenerateToken(User user);

    Guid GetLoggedInUserId();

    string? GetToken();
}

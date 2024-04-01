using Portfolio.Domain.Dtos;

namespace Portfolio.Domain.Interfaces;

public interface IAuthService
{
    TokenDto GenerateToken(UserDto user);

    Guid GetLoggedInUserId();

    string? GetToken();
}

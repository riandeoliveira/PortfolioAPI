using Portfolio.Domain.Dtos;

namespace Portfolio.Domain.Interfaces;

public interface IAuthService
{
    string GenerateToken(UserDto user);

    Guid GetLoggedInUserId();

    string? GetToken();
}

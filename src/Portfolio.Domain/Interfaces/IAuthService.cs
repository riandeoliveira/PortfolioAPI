using Portfolio.Domain.Dtos;

namespace Portfolio.Domain.Interfaces;

public interface IAuthService
{
    Task<string> GenerateRefreshTokenAsync(UserDto user, CancellationToken cancellationToken = default);

    Task<TokenDto> GenerateTokenDataAsync(UserDto user, CancellationToken cancellationToken = default);

    Task<string> GetAccessTokenFromHeaderAsync(CancellationToken cancellationToken = default);

    Task<UserDto> GetCurrentUserAsync(CancellationToken cancellationToken = default);

    Task<UserDto> GetUserFromTokenAsync(string accessToken, CancellationToken cancellationToken = default);
}

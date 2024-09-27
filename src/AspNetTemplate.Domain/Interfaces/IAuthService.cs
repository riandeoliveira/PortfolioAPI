using System.Security.Claims;

using AspNetTemplate.Domain.Dtos;

namespace AspNetTemplate.Domain.Interfaces;

public interface IAuthService
{
    void ClearJwtCookies();

    JwtTokenDto CreateJwtTokenData(Guid userId);

    Guid? FindAuthenticatedUserId();

    (string? AccessToken, string? RefreshToken) GetJwtCookies();

    void SendJwtCookiesToClient(JwtTokenDto jwtTokenDto);

    ClaimsPrincipal ValidateJwtTokenOrThrow(string? token);
}

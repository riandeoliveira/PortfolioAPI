using AspNetTemplate.Infra.Data.Dtos;

using System.Security.Claims;

namespace AspNetTemplate.Infra.Data.Interfaces;

public interface IAuthService
{
    void ClearJwtCookies();

    JwtTokenDto CreateJwtTokenData(Guid userId);

    Guid? FindAuthenticatedUserId();

    (string? AccessToken, string? RefreshToken) GetJwtCookies();

    void SendJwtCookiesToClient(JwtTokenDto jwtTokenDto);

    ClaimsPrincipal ValidateJwtTokenOrThrow(string? token);
}

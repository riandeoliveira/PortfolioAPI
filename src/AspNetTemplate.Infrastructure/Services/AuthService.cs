using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

using AspNetTemplate.Domain.Constants;
using AspNetTemplate.Domain.Dtos;
using AspNetTemplate.Domain.Interfaces;
using AspNetTemplate.Domain.Exceptions;
using AspNetTemplate.Domain.Enums;

namespace AspNetTemplate.Infrastructure.Services;

public sealed class AuthService(IHttpContextAccessor httpContextAccessor) : IAuthService
{
    private readonly JwtSecurityTokenHandler _tokenHandler = new();
    private readonly HttpRequest? _httpRequest = httpContextAccessor.HttpContext?.Request;
    private readonly HttpResponse? _httpResponse = httpContextAccessor.HttpContext?.Response;

    private static CookieOptions CreateCookieOptions(DateTime expires)
    {
        return new CookieOptions()
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = expires
        };
    }

    private TokenDto CreateTokenData(Guid userId, DateTime expiration)
    {
        Claim[] claims =
        [
            new("id", userId.ToString()),
        ];

        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(EnvironmentVariables.JWT_SECRET));
        SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256Signature);

        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Issuer = EnvironmentVariables.JWT_ISSUER,
            Audience = EnvironmentVariables.JWT_AUDIENCE,
            Subject = new ClaimsIdentity(claims),
            Expires = expiration,
            SigningCredentials = credentials
        };

        SecurityToken securityToken = _tokenHandler.CreateToken(tokenDescriptor);

        string token = _tokenHandler.WriteToken(securityToken);

        return new TokenDto(token, expiration);
    }

    public void ClearJwtCookies()
    {
        _httpResponse?.Cookies.Delete("access_token");
        _httpResponse?.Cookies.Delete("refresh_token");
    }

    public JwtTokenDto CreateJwtTokenData(Guid userId)
    {
        TokenDto accessToken = CreateTokenData(userId, DateTime.UtcNow.AddMinutes(30));
        TokenDto refreshToken = CreateTokenData(userId, DateTime.UtcNow.AddDays(1));

        return new JwtTokenDto(accessToken, refreshToken);
    }

    public Guid? FindAuthenticatedUserId()
    {
        (string? accessToken, _) = GetJwtCookies();

        if (accessToken is not null)
        {
            ClaimsPrincipal principal = ValidateJwtTokenOrThrow(accessToken);

            string userId = principal.FindFirst("id")?.Value ?? string.Empty;

            if (!string.IsNullOrEmpty(userId)) return Guid.Parse(userId);
        }

        return null;
    }

    public (string? AccessToken, string? RefreshToken) GetJwtCookies()
    {
        if (_httpRequest is null) return (null, null);

        _httpRequest.Cookies.TryGetValue("access_token", out string? accessToken);
        _httpRequest.Cookies.TryGetValue("refresh_token", out string? refreshToken);

        return (accessToken, refreshToken);
    }

    public void SendJwtCookiesToClient(JwtTokenDto jwtTokenDto)
    {
        _httpResponse?.Cookies.Append(
            "access_token",
            jwtTokenDto.AccessToken.Value,
            CreateCookieOptions(DateTime.UtcNow.AddMinutes(30))
        );

        _httpResponse?.Cookies.Append(
            "refresh_token",
            jwtTokenDto.RefreshToken.Value,
            CreateCookieOptions(DateTime.UtcNow.AddHours(1))
        );
    }

    public ClaimsPrincipal ValidateJwtTokenOrThrow(string? token)
    {
        TokenValidationParameters parameters = new()
        {
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(EnvironmentVariables.JWT_SECRET)),
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidAudience = EnvironmentVariables.JWT_AUDIENCE,
            ValidIssuer = EnvironmentVariables.JWT_ISSUER,
        };

        try
        {
            ClaimsPrincipal? principal = _tokenHandler.ValidateToken(token, parameters, out _);

            return principal;
        }
        catch
        {
            throw new UnauthorizedException(Message.UnauthorizedOperation);
        }

    }
}

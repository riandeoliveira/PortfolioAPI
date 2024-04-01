using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

using Portfolio.Domain.Constants;
using Portfolio.Domain.Dtos;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Infrastructure.Services;

public sealed class AuthService(IHttpContextAccessor httpContextAccessor) : IAuthService
{
    private readonly JwtSecurityTokenHandler _tokenHandler = new();

    public TokenDto GenerateToken(UserDto user)
    {
        Claim[] claims =
        [
            new Claim("id", user.Id.ToString()),
            new Claim("email", user.Email)
        ];

        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(EnvironmentVariables.JWT_SECRET));
        SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256Signature);

        DateTime expires = DateTime.UtcNow.AddHours(24);

        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Issuer = EnvironmentVariables.JWT_ISSUER,
            Audience = EnvironmentVariables.JWT_AUDIENCE,
            Subject = new ClaimsIdentity(claims),
            Expires = expires,
            SigningCredentials = credentials
        };

        SecurityToken securityToken = _tokenHandler.CreateToken(tokenDescriptor);
        string token = _tokenHandler.WriteToken(securityToken);

        return new TokenDto(
            Token: $"Bearer {token}",
            RefreshToken: $"Bearer {token}",
            Expires: new DateTimeOffset(expires).ToUnixTimeSeconds(),
            UserId: user.Id
        );
    }

    public Guid GetLoggedInUserId()
    {
        JwtPayload payload = _tokenHandler.ReadJwtToken(GetToken()).Payload;
        string? userIdPayload = payload["id"] as string;
        bool isValid = Guid.TryParse(userIdPayload, out Guid userId);

        return isValid ? userId : Guid.Empty;
    }

    public string? GetToken()
    {
        string? authorizationHeader = httpContextAccessor.HttpContext?.Request.Headers.Authorization.ToString();

        if (string.IsNullOrWhiteSpace(authorizationHeader)) return null;

        string? token = authorizationHeader.Split(" ").LastOrDefault();

        return token;
    }
}

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

using Portfolio.Domain.Entities;
using Portfolio.Utils.Constants;
using Portfolio.Utils.Interfaces;

namespace Portfolio.Utils.Services;

public sealed class AuthService(IHttpContextAccessor httpContextAccessor) : IAuthService
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly JwtSecurityTokenHandler _tokenHandler = new();
    private readonly SymmetricSecurityKey _securityKey = new(Encoding.ASCII.GetBytes(EnvironmentVariables.JWT_SECRET));

    public string GenerateToken(User user)
    {
        Claim[] claims =
        [
            new("id", user.Id.ToString()),
            new("email", user.Email)
        ];

        SigningCredentials credentials = new(_securityKey, SecurityAlgorithms.HmacSha256Signature);

        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(claims),
            SigningCredentials = credentials
        };

        SecurityToken token = _tokenHandler.CreateToken(tokenDescriptor);
        string result = _tokenHandler.WriteToken(token);

        return $"Bearer {result}";
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
        string? authorizationHeader = _httpContextAccessor.HttpContext?.Request.Headers.Authorization.ToString();

        if (string.IsNullOrWhiteSpace(authorizationHeader)) return null;

        string? token = authorizationHeader.Split(" ").LastOrDefault();

        return token;
    }
}

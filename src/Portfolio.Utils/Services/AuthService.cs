using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using Portfolio.Domain.Entities;
using Portfolio.Utils.Interfaces;

namespace Portfolio.Utils.Services;

public sealed class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly JwtSecurityTokenHandler _tokenHandler;
    private readonly SymmetricSecurityKey _securityKey;

    public AuthService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
        _securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JwtSettings:Secret"] ?? ""));
        _tokenHandler = new JwtSecurityTokenHandler();
    }

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

    public string? GetToken()
    {
        string? authorizationHeader = _httpContextAccessor.HttpContext?.Request.Headers.Authorization.ToString();

        if (string.IsNullOrWhiteSpace(authorizationHeader)) return null;

        string? token = authorizationHeader.Split(" ").LastOrDefault();

        return token;
    }
}

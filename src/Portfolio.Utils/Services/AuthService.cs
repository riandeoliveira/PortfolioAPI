using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using Portfolio.Entities;
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

        _tokenHandler = new JwtSecurityTokenHandler();

        _securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JwtSettings:Secret"] ?? ""));
    }

    public string GenerateToken(User user)
    {
        var claims = new Claim[]
        {
            new("id", user.Id.ToString()),
            new("email", user.Email)
        };

        var credentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            SigningCredentials = credentials
        };

        var token = _tokenHandler.CreateToken(tokenDescriptor);
        var result = _tokenHandler.WriteToken(token);

        return $"Bearer {result}";
    }

    public string GetToken()
    {
        var authorizationHeader = _httpContextAccessor.HttpContext?.Request.Headers.Authorization.ToString();

        if (string.IsNullOrWhiteSpace(authorizationHeader))
        {
            return null;
        }

        var token = authorizationHeader.Split(" ").LastOrDefault();

        return token;
    }
}

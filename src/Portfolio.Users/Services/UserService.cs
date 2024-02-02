using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using Portfolio.Entities;
using Portfolio.Users.Interfaces;
using Portfolio.Users.Requests;
using Portfolio.Users.Responses;

namespace Portfolio.Users.Services;

public sealed class UserService(IConfiguration configuration, IUserRepository repository) : IUserService
{
    private readonly IConfiguration _configuration = configuration;
    private readonly IUserRepository _repository = repository;

    public async Task<TokenResponse?> LoginAsync(LoginUserRequest request)
    {
        var user = await _repository.FindAsync(x =>
            x.Email == request.Email &&
            x.Password == request.Password
        );

        if (user is not null)
        {
            var token = GenerateToken(user);

            return new TokenResponse
            {
                Token = token,
                UserId = user.Id
            };
        }

        return null;
    }

    public async Task<TokenResponse> SignInAsync(SignInUserRequest request)
    {
        var userAlreadyExists = await _repository.FindAsync(x => x.Email == request.Email);

        if (userAlreadyExists is not null)
        {
            throw new InvalidOperationException("Email already exists");
        }

        var user = new User
        {
            Email = request.Email,
            Password = request.Password
        };

        var createdUser = await _repository.CreateAsync(user);

        await _repository.SaveChangesAsync();

        var token = GenerateToken(createdUser);

        return new TokenResponse
        {
            Token = token,
            UserId = createdUser.Id
        };
    }

    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Secret"] ?? "");

        var securityKey = new SymmetricSecurityKey(key);
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var claims = new Claim[]
        {
            new("id", user.Id.ToString()),
            new("email", user.Email)
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            SigningCredentials = credentials
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var result = tokenHandler.WriteToken(token);

        return $"Bearer {result}";
    }

    public ClaimsPrincipal? GetPrincipal(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Secret"]);

        try
        {
            var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _configuration["JwtSettings:Issuer"],
                ValidAudience = _configuration["JwtSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validaterequestken);

            return principal;
        }
        catch
        {
            return null;
        }
    }
}

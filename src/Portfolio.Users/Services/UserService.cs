using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using Portfolio.Entities;
using Portfolio.Users.Interfaces;
using Portfolio.Users.Requests;
using Portfolio.Users.Responses;
using Portfolio.Utils.Extensions;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;

namespace Portfolio.Users.Services;

public sealed class UserService(IConfiguration configuration, IUserRepository repository) : IUserService
{
    private readonly IConfiguration _configuration = configuration;

    private readonly IUserRepository _repository = repository;

    public async Task<TokenResponse> LoginAsync(LoginUserRequest request)
    {
        var user = await _repository.FindAsync(x =>
            x.Email == request.Email
        );

        if (user is not null && PasswordExtension.VerifyPassword(request.Password, user.Password))
        {
            var token = GenerateToken(user);

            return new TokenResponse
            {
                Token = token,
                UserId = user.Id
            };
        }

        throw new InvalidCredentialException("Invalid email or password");
    }

    public async Task<TokenResponse> SignInAsync(SignInUserRequest request)
    {
        var userFound = await _repository.FindAsync(x => x.Email == request.Email);

        if (userFound is not null)
        {
            throw new Exception("Email already exists");
        }

        var hashedPassword = PasswordExtension.HashPassword(request.Password);

        var user = new User
        {
            Email = request.Email,
            Password = hashedPassword
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

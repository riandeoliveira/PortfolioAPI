using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

using Portfolio.Domain.Constants;
using Portfolio.Domain.Dtos;
using Portfolio.Domain.Enums;
using Portfolio.Domain.Exceptions;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Infrastructure.Services;

public sealed class AuthService(
    IHttpContextAccessor httpContextAccessor,
    ILocalizationService localizationService
) : IAuthService
{
    private readonly JwtSecurityTokenHandler _tokenHandler = new();

    public async Task<string> GenerateRefreshTokenAsync(UserDto user, CancellationToken cancellationToken = default)
    {
        return await Task.Run(() =>
        {
            byte[] randomNumber = new byte[32];

            using RandomNumberGenerator generator = RandomNumberGenerator.Create();

            generator.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }, cancellationToken);
    }

    public async Task<TokenDto> GenerateTokenDataAsync(UserDto user, CancellationToken cancellationToken = default)
    {
        return await Task.Run(async () =>
        {
            Claim[] claims =
            [
                new("id", user.Id.ToString()),
                new("email", user.Email)
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

            string accessToken = _tokenHandler.WriteToken(securityToken);
            string refreshToken = await GenerateRefreshTokenAsync(user, cancellationToken);

            return new TokenDto(
                AccessToken: $"Bearer {accessToken}",
                RefreshToken: refreshToken,
                Expires: new DateTimeOffset(expires).ToUnixTimeSeconds(),
                UserId: user.Id
            );
        }, cancellationToken);
    }

    private async Task<string> GetAccessTokenFromHeaderAsync(CancellationToken cancellationToken = default)
    {
        return await Task.Run(() =>
        {
            string? authorizationHeader = httpContextAccessor.HttpContext?.Request.Headers.Authorization;
            string? accessToken = authorizationHeader?.Replace("Bearer ", "");

            return accessToken is not null
                ? accessToken
                : throw new BaseException(localizationService.GetKey(LocalizationMessages.AccessTokenIsRequired));
        }, cancellationToken);
    }

    public async Task<UserDto> GetCurrentUserAsync(CancellationToken cancellationToken = default)
    {
        return await Task.Run(async () =>
        {
            string accessToken = await GetAccessTokenFromHeaderAsync(cancellationToken);

            return ParseUserFromAccessToken(accessToken);
        }, cancellationToken);
    }

    public async Task<UserDto> GetUserFromAccessTokenAsync(string accessToken, CancellationToken cancellationToken = default)
    {
        return await Task.Run(() => ParseUserFromAccessToken(accessToken), cancellationToken);
    }

    private UserDto ParseUserFromAccessToken(string accessToken)
    {
        string jwtToken = accessToken.Replace("Bearer ", "");

        JwtPayload payload = _tokenHandler.ReadJwtToken(jwtToken).Payload;

        string userIdPayload = (payload["id"] as string)!;
        string userEmailPayload = (payload["email"] as string)!;

        _ = Guid.TryParse(userIdPayload, out Guid userId);

        return new UserDto(userId, userEmailPayload);
    }
}

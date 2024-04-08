using Microsoft.AspNetCore.Http;

using Moq;

using Portfolio.Domain.Dtos;
using Portfolio.Domain.Interfaces;
using Portfolio.Domain.Tests.Extensions;
using Portfolio.Infrastructure.Services;

using System.Net.Http.Headers;
using Portfolio.Domain.Tests.Fixtures;
using Portfolio.Application.UseCases.SignInUser;

namespace Portfolio.Domain.Tests.Helper;

public sealed class AuthHelper(HttpClient client)
{
    private static readonly DefaultHttpContext HttpContext = new();

    private static AuthService AuthServiceMock
    {
        get
        {
            Mock<IHttpContextAccessor> httpContextAccessorMock = new();
            Mock<ILocalizationService> localizationServiceMock = new();

            httpContextAccessorMock.Setup(accessor => accessor.HttpContext).Returns(HttpContext);

            AuthService authService = new(
                httpContextAccessorMock.Object,
                localizationServiceMock.Object
            );

            return authService;
        }
    }

    public async Task<(SignInUserRequest request, TokenDto body)> AuthenticateAsync()
    {
        SignInUserRequest request = new(
            DatabaseFixture.User.Email,
            DatabaseFixture.User.Password
        );

        HttpResponseMessage response = await client.SendPostAsync("/api/user/sign-in", request);

        TokenDto body = await response.GetBodyAsync<TokenDto>();

        SetAccessTokenInHeader(body.AccessToken);

        return (request, body);
    }

    public static async Task<string> GenerateRefreshTokenAsync(UserDto userDto)
    {
        return await AuthServiceMock.GenerateRefreshTokenAsync(userDto);
    }

    public static async Task<TokenDto> GenerateTokenDataAsync(UserDto userDto)
    {
        return await AuthServiceMock.GenerateTokenDataAsync(userDto);
    }

    public static async Task<UserDto> GetCurrentUserAsync()
    {
        return await AuthServiceMock.GetCurrentUserAsync();
    }

    public static async Task<UserDto> GetUserFromAccessTokenAsync(string accessToken)
    {
        return await AuthServiceMock.GetUserFromAccessTokenAsync(accessToken);
    }

    public void SetAccessTokenInHeader(string? accessToken)
    {
        string? token = accessToken?.Replace("Bearer ", "");

        HttpContext.Request.Headers.Authorization = accessToken;

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
}

using System.Net.Http.Headers;

using Microsoft.AspNetCore.Http;

using Moq;

using Portfolio.Application.UseCases.SignInUser;
using Portfolio.Domain.Dtos;
using Portfolio.Domain.Interfaces;
using Portfolio.Domain.Tests.Extensions;
using Portfolio.Domain.Tests.Factories;
using Portfolio.Domain.Tests.Fixtures;
using Portfolio.Infrastructure.Services;

namespace Portfolio.Domain.Tests.Common;

public abstract class BaseAuthTest(PortfolioWebApplicationFactory factory) : BaseTest(factory)
{
    private static readonly DefaultHttpContext HttpContext = new();

    private static AuthService AuthServiceMock
    {
        get
        {
            Mock<IHttpContextAccessor> httpContextAccessorMock = new();

            httpContextAccessorMock.Setup(accessor => accessor.HttpContext).Returns(HttpContext);

            AuthService authService = new(
                httpContextAccessorMock.Object
            );

            return authService;
        }
    }

    protected async Task AuthenticateAsync()
    {
        SignInUserRequest request = new(
            DatabaseFixture.User_1.Email,
            DatabaseFixture.User_1.Password
        );

        HttpResponseMessage response = await _client.SendPostAsync("/api/user/sign-in", request);

        TokenDto body = await response.GetBodyAsync<TokenDto>();

        SetAccessTokenInHeader(body.AccessToken);
    }

    protected static async Task<string> GenerateRefreshTokenAsync(UserDto userDto)
    {
        return await AuthServiceMock.GenerateRefreshTokenAsync(userDto);
    }

    protected static async Task<TokenDto> GenerateTokenDataAsync(UserDto userDto)
    {
        return await AuthServiceMock.GenerateTokenDataAsync(userDto);
    }

    protected static async Task<UserDto> GetCurrentUserAsync()
    {
        return await AuthServiceMock.GetCurrentUserAsync();
    }

    protected static async Task<UserDto> GetUserFromAccessTokenAsync(string accessToken)
    {
        return await AuthServiceMock.GetUserFromAccessTokenAsync(accessToken);
    }

    protected void SetAccessTokenInHeader(string? accessToken)
    {
        string? token = accessToken?.Replace("Bearer ", "");

        HttpContext.Request.Headers.Authorization = accessToken;

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
}

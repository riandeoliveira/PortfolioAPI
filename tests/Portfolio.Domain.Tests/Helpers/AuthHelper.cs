using System.Net.Http.Json;

using Bogus;

using Microsoft.AspNetCore.Http;

using Moq;

using Portfolio.Application.UseCases.SignUpUser;
using Portfolio.Domain.Dtos;
using Portfolio.Domain.Interfaces;
using Portfolio.Domain.Tests.Extensions;
using Portfolio.Infrastructure.Services;

using Portfolio.Infrastructure.Extensions;
using System.Net.Http.Headers;

namespace Portfolio.Domain.Tests.Helper;

public sealed class AuthHelper(HttpClient client)
{
    private static readonly DefaultHttpContext HttpContext = new();

    public static IAuthService AuthServiceMock
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

    public async Task<(SignUpUserRequest request, TokenDto body)> AuthenticateAsync()
    {
        Faker faker = new();

        SignUpUserRequest request = new(
            faker.Internet.Email(),
            faker.Internet.StrongPassword()
        );

        HttpResponseMessage response = await client.PostAsJsonAsync("/api/user/sign-up", request);

        TokenDto body = await response.GetBodyAsync<TokenDto>();

        SetAccessTokenInHeader(body.AccessToken);

        return (request, body);
    }

    // public static string? GetAccessTokenFromHeader()
    // {
    //     return HttpContext.Request.Headers.Authorization;
    // }

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

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
}

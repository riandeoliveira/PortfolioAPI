using System.Net.Http.Json;

using FluentAssertions;

using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

using Moq;

using Portfolio.Application.UseCases.SignUpUser;
using Portfolio.Domain.Dtos;
using Portfolio.Domain.Exceptions;
using Portfolio.Domain.Interfaces;
using Portfolio.Domain.Tests.Common;
using Portfolio.Domain.Tests.Extensions;
using Portfolio.Domain.Tests.Factories;
using Portfolio.Infrastructure.Services;

using Portolio.Infrastructure.Extensions;

namespace Portfolio.Infrastructure.Tests.Services.Auth;

public sealed class GetCurrentUserAsyncTest(PortfolioWebApplicationFactory factory) : BaseTest(factory)
{
    [Fact]
    public async Task ShouldGetCurrentUser()
    {
        SignUpUserRequest request = new(
            _faker.Internet.Email(),
            _faker.Internet.StrongPassword()
        );

        HttpResponseMessage response = await _client.PostAsJsonAsync("/api/user/sign-up", request);

        TokenDto body = await response.GetBody<TokenDto>();

        DefaultHttpContext httpContext = new();

        string accessToken = body.AccessToken;

        httpContext.Request.Headers.Authorization = accessToken;

        Mock<IHttpContextAccessor> httpContextAccessorMock = new();
        Mock<ILocalizationService> localizationServiceMock = new();

        httpContextAccessorMock.Setup(accessor => accessor.HttpContext).Returns(httpContext);

        AuthService authService = new(
            httpContextAccessorMock.Object,
            localizationServiceMock.Object
        );

        UserDto user = await authService.GetCurrentUserAsync();

        user.Id.Should().NotBe(Guid.Empty);
        user.Id.Should().Be(body.UserId);

        user.Email.Should().NotBeNullOrWhiteSpace();
        user.Email.Should().Be(request.Email);
    }

    [Fact]
    public async Task ShouldThrowsWithInvalidAccessToken()
    {
        DefaultHttpContext httpContext = new();

        string invalidAccessToken = _faker.Random.Word();

        httpContext.Request.Headers.Authorization = invalidAccessToken;

        Mock<IHttpContextAccessor> httpContextAccessorMock = new();
        Mock<ILocalizationService> localizationServiceMock = new();

        httpContextAccessorMock.Setup(accessor => accessor.HttpContext).Returns(httpContext);

        AuthService authService = new(
            httpContextAccessorMock.Object,
            localizationServiceMock.Object
        );

        Func<Task> action = async () => await authService.GetCurrentUserAsync();

        await action.Should().ThrowAsync<SecurityTokenMalformedException>();
    }

    [Fact]
    public async Task ShouldThrowsWithoutAccessToken()
    {
        DefaultHttpContext httpContext = new();

        Mock<IHttpContextAccessor> httpContextAccessorMock = new();
        Mock<ILocalizationService> localizationServiceMock = new();

        httpContextAccessorMock.Setup(accessor => accessor.HttpContext).Returns(httpContext);

        AuthService authService = new(
            httpContextAccessorMock.Object,
            localizationServiceMock.Object
        );

        Func<Task> action = async () => await authService.GetCurrentUserAsync();

        await action.Should().ThrowAsync<BaseException>();
    }
}

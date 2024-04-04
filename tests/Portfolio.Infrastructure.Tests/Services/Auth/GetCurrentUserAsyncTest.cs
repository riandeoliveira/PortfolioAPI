using FluentAssertions;

using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

using Moq;

using Portfolio.Domain.Exceptions;
using Portfolio.Domain.Interfaces;
using Portfolio.Domain.Tests.Common;
using Portfolio.Domain.Tests.Factories;
using Portfolio.Infrastructure.Services;

namespace Portfolio.Infrastructure.Tests.Services.Auth;

public sealed class GetCurrentUserAsyncTest(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task ShouldThrowsWithInvalidAccessToken()
    {
        DefaultHttpContext httpContext = new();

        string invalidAccessToken = _faker.Random.Word();

        httpContext.Request.Headers.Authorization = invalidAccessToken;

        Mock<IHttpContextAccessor> httpContextAccessorMock = new();
        Mock<ILocalizationService> localizationServiceMock = new();

        httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

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

        httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

        AuthService authService = new(
            httpContextAccessorMock.Object,
            localizationServiceMock.Object
        );

        Func<Task> action = async () => await authService.GetCurrentUserAsync();

        await action.Should().ThrowAsync<BaseException>();
    }
}

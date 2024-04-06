using FluentAssertions;

using Microsoft.IdentityModel.Tokens;

using Portfolio.Application.UseCases.SignUpUser;
using Portfolio.Domain.Dtos;
using Portfolio.Domain.Exceptions;
using Portfolio.Domain.Tests.Common;
using Portfolio.Domain.Tests.Factories;
using Portfolio.Domain.Tests.Helper;

namespace Portfolio.Infrastructure.Tests.Services.Auth;

public sealed class GetCurrentUserAsyncTest(PortfolioWebApplicationFactory factory) : BaseTest(factory)
{
    [Fact]
    public async Task ShouldGetCurrentUser()
    {
        AuthHelper authHelper = new(_client);

        (SignUpUserRequest request, TokenDto body) = await authHelper.AuthenticateAsync();

        UserDto user = await AuthHelper.AuthServiceMock.GetCurrentUserAsync();

        user.Id.Should().NotBe(Guid.Empty);
        user.Id.Should().Be(body.UserId);

        user.Email.Should().NotBeNullOrWhiteSpace();
        user.Email.Should().Be(request.Email);
    }

    [Fact]
    public async Task ShouldThrowsWithInvalidAccessToken()
    {
        string invalidAccessToken = _faker.Random.Word();

        AuthHelper.SetAccessTokenInHeader(invalidAccessToken);

        Func<Task> action = async () => await AuthHelper.AuthServiceMock.GetCurrentUserAsync();

        await action.Should().ThrowAsync<SecurityTokenMalformedException>();
    }

    [Fact]
    public async Task ShouldThrowsWithoutAccessToken()
    {
        AuthHelper.SetAccessTokenInHeader(null);

        Func<Task> action = async () => await AuthHelper.AuthServiceMock.GetCurrentUserAsync();

        await action.Should().ThrowAsync<BaseException>();
    }
}

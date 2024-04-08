using Bogus;

using FluentAssertions;

using Microsoft.IdentityModel.Tokens;

using Portfolio.Application.UseCases.SignInUser;
using Portfolio.Domain.Dtos;
using Portfolio.Domain.Exceptions;
using Portfolio.Domain.Tests.Common;
using Portfolio.Domain.Tests.Factories;
using Portfolio.Domain.Tests.Fixtures;

namespace Portfolio.Infrastructure.Tests.Services.Auth;

public sealed class GetCurrentUserAsyncTest(PortfolioWebApplicationFactory factory) : BaseAuthTest(factory)
{
    [Fact]
    public async Task ShouldGet_CurrentUser()
    {
        await AuthenticateAsync();

        UserDto user = await GetCurrentUserAsync();

        user.Id.Should().NotBe(Guid.Empty);
        user.Id.Should().Be(DatabaseFixture.User_1.Id);

        user.Email.Should().NotBeNullOrWhiteSpace();
        user.Email.Should().Be(DatabaseFixture.User_1.Email);
    }

    [Fact]
    public async Task ShouldThrows_WithInvalidAccessToken()
    {
        string invalidAccessToken = _faker.Random.Word();

        SetAccessTokenInHeader(invalidAccessToken);

        Func<Task> action = GetCurrentUserAsync;

        await action.Should().ThrowAsync<SecurityTokenMalformedException>();
    }

    [Fact]
    public async Task ShouldThrows_WithoutAccessToken()
    {
        SetAccessTokenInHeader(null);

        Func<Task> action = GetCurrentUserAsync;

        await action.Should().ThrowAsync<BaseException>();
    }
}

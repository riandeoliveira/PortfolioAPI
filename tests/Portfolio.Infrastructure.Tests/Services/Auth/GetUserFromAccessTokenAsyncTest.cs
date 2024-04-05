using FluentAssertions;

using Microsoft.IdentityModel.Tokens;

using Portfolio.Domain.Dtos;
using Portfolio.Domain.Tests.Common;
using Portfolio.Domain.Tests.Factories;

namespace Portfolio.Infrastructure.Tests.Services.Auth;

public sealed class GetUserFromAccessTokenAsyncTest(PortfolioWebApplicationFactory factory) : BaseTest(factory)
{
    [Fact]
    public async Task ShouldGetValidUser()
    {
        UserDto userDto = new(_faker.Random.Guid(), _faker.Internet.Email());

        TokenDto tokenDto = await _authService.GenerateTokenDataAsync(userDto);

        UserDto userFromToken = await _authService.GetUserFromAccessTokenAsync(tokenDto.AccessToken);

        userFromToken.Id.Should().NotBe(Guid.Empty);
        userFromToken.Id.Should().Be(userDto.Id);

        userFromToken.Email.Should().NotBeNullOrWhiteSpace();
        userFromToken.Email.Should().Be(userDto.Email);
    }

    [Fact]
    public async Task ShouldThrowsWithInvalidAccessToken()
    {
        string invalidAccessToken = _faker.Random.Word();

        Func<Task> action = async () => await _authService.GetUserFromAccessTokenAsync(invalidAccessToken);

        await action.Should().ThrowAsync<SecurityTokenMalformedException>();
    }
}

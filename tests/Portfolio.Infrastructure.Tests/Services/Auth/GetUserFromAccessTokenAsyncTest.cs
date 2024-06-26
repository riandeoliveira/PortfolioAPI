using FluentAssertions;

using Microsoft.IdentityModel.Tokens;

using Portfolio.Domain.Dtos;
using Portfolio.Domain.Tests.Common;
using Portfolio.Domain.Tests.Factories;

namespace Portfolio.Infrastructure.Tests.Services.Auth;

public sealed class GetUserFromAccessTokenAsyncTest(PortfolioWebApplicationFactory factory) : BaseAuthTest(factory)
{
    [Fact]
    public async Task ShouldGet_ValidUser()
    {
        UserDto userDto = new(_faker.Random.Guid(), _faker.Internet.Email());

        TokenDto tokenDto = await GenerateTokenDataAsync(userDto);

        UserDto userFromToken = await GetUserFromAccessTokenAsync(tokenDto.AccessToken);

        userFromToken.Id.Should().NotBe(Guid.Empty);
        userFromToken.Id.Should().Be(userDto.Id);

        userFromToken.Email.Should().NotBeNullOrWhiteSpace();
        userFromToken.Email.Should().Be(userDto.Email);
    }

    [Fact]
    public async Task ShouldThrows_WithInvalidAccessToken()
    {
        string invalidAccessToken = _faker.Random.Word();

        Func<Task> action = async () => await GetUserFromAccessTokenAsync(invalidAccessToken);

        await action.Should().ThrowAsync<SecurityTokenMalformedException>();
    }
}

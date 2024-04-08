using FluentAssertions;

using Portfolio.Domain.Dtos;
using Portfolio.Domain.Tests.Common;
using Portfolio.Domain.Tests.Factories;

namespace Portfolio.Infrastructure.Tests.Services.Auth;

public sealed class GenerateTokenDataAsyncTest(PortfolioWebApplicationFactory factory) : BaseAuthTest(factory)
{
    [Fact]
    public async Task ShouldGenerate_UniqueAccessTokens()
    {
        UserDto firstUserDto = new(_faker.Random.Guid(), _faker.Internet.Email());
        UserDto secondUserDto = new(_faker.Random.Guid(), _faker.Internet.Email());

        TokenDto firstTokenDto = await GenerateTokenDataAsync(firstUserDto);
        TokenDto secondTokenDto = await GenerateTokenDataAsync(secondUserDto);

        firstTokenDto.AccessToken.Should().NotBe(secondTokenDto.AccessToken);
    }

    [Fact]
    public async Task ShouldGenerate_ValidTokenData()
    {
        UserDto userDto = new(_faker.Random.Guid(), _faker.Internet.Email());

        TokenDto tokenDto = await GenerateTokenDataAsync(userDto);

        long now = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();

        tokenDto.AccessToken.Should().NotBeNullOrWhiteSpace();
        tokenDto.AccessToken.Should().StartWith("Bearer");

        tokenDto.RefreshToken.Should().NotBeNullOrWhiteSpace();

        tokenDto.Expires.Should().BeGreaterThan(now);

        tokenDto.UserId.Should().NotBe(Guid.Empty);
        tokenDto.UserId.Should().Be(userDto.Id);
    }
}

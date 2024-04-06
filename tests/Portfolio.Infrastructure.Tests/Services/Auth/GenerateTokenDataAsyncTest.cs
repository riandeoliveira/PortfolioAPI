using FluentAssertions;

using Portfolio.Domain.Dtos;
using Portfolio.Domain.Tests.Common;
using Portfolio.Domain.Tests.Factories;
using Portfolio.Domain.Tests.Helper;

namespace Portfolio.Infrastructure.Tests.Services.Auth;

public sealed class GenerateTokenDataAsyncTest(PortfolioWebApplicationFactory factory) : BaseTest(factory)
{
    [Fact]
    public async Task ShouldGenerateUniqueAccessTokens()
    {
        UserDto firstUserDto = new(_faker.Random.Guid(), _faker.Internet.Email());
        UserDto secondUserDto = new(_faker.Random.Guid(), _faker.Internet.Email());

        TokenDto firstTokenDto = await AuthHelper.AuthServiceMock.GenerateTokenDataAsync(firstUserDto);
        TokenDto secondTokenDto = await AuthHelper.AuthServiceMock.GenerateTokenDataAsync(secondUserDto);

        firstTokenDto.AccessToken.Should().NotBe(secondTokenDto.AccessToken);
    }

    [Fact]
    public async Task ShouldGenerateValidTokenData()
    {
        UserDto userDto = new(_faker.Random.Guid(), _faker.Internet.Email());

        TokenDto tokenDto = await AuthHelper.AuthServiceMock.GenerateTokenDataAsync(userDto);

        long now = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();

        tokenDto.AccessToken.Should().NotBeNullOrWhiteSpace();
        tokenDto.AccessToken.Should().StartWith("Bearer");

        tokenDto.RefreshToken.Should().NotBeNullOrWhiteSpace();

        tokenDto.Expires.Should().BeGreaterThan(now);

        tokenDto.UserId.Should().NotBe(Guid.Empty);
        tokenDto.UserId.Should().Be(userDto.Id);
    }
}

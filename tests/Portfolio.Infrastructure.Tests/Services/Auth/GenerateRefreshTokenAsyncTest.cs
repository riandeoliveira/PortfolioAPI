using FluentAssertions;

using Portfolio.Domain.Dtos;
using Portfolio.Domain.Tests.Common;
using Portfolio.Domain.Tests.Factories;

namespace Portfolio.Infrastructure.Tests.Services.Auth;

public sealed class GenerateRefreshTokenAsyncTest(PortfolioWebApplicationFactory factory) : BaseTest(factory)
{
    [Fact]
    public async Task ShouldGenerateUniqueRefreshTokens()
    {
        UserDto userDto = new(_faker.Random.Guid(), _faker.Internet.Email());

        string firstRefreshToken = await _authService.GenerateRefreshTokenAsync(userDto);
        string secondRefreshToken = await _authService.GenerateRefreshTokenAsync(userDto);

        firstRefreshToken.Should().NotBe(secondRefreshToken);
    }

    [Fact]
    public async Task ShouldGenerateValidRefreshToken()
    {
        UserDto userDto = new(_faker.Random.Guid(), _faker.Internet.Email());

        string refreshToken = await _authService.GenerateRefreshTokenAsync(userDto);

        int expectedLength = 44;

        refreshToken.Should().NotBeNullOrWhiteSpace();
        refreshToken.Length.Should().Be(expectedLength);
    }
}

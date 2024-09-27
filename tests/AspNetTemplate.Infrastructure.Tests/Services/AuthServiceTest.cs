using AspNetTemplate.Domain.Dtos;
using AspNetTemplate.Domain.Tests.Common;
using AspNetTemplate.Domain.Tests.Factories;

using FluentAssertions;

namespace AspNetTemplate.Infrastructure.Tests.Services;

public sealed class AuthServiceTest(AspNetTemplateWebApplicationFactory factory) : BaseTest(factory)
{
    [Fact]
    public void ClearJwtCookies_Test()
    {
    }

    [Fact]
    public void CreateJwtTokenData_ShouldGenerate_UniqueTokens()
    {
        Guid firstUserId = _faker.Random.Guid();
        Guid secondUserId = _faker.Random.Guid();

        JwtTokenDto firstJwtTokenDto = _authService.CreateJwtTokenData(firstUserId);
        JwtTokenDto secondJwtTokenDto = _authService.CreateJwtTokenData(secondUserId);

        firstJwtTokenDto.AccessToken.Value.Should().NotBe(secondJwtTokenDto.AccessToken.Value);
        firstJwtTokenDto.AccessToken.Value.Should().NotBe(secondJwtTokenDto.RefreshToken.Value);
    }

    [Fact]
    public void CreateJwtTokenData_ShouldGenerate_ValidTokens()
    {
        Guid userId = _faker.Random.Guid();

        JwtTokenDto jwtTokenDto = _authService.CreateJwtTokenData(userId);

        jwtTokenDto.AccessToken.Value.Should().NotBeNullOrWhiteSpace();
        jwtTokenDto.AccessToken.ExpiresIn.Should().BeAfter(DateTime.Now);

        jwtTokenDto.RefreshToken.Value.Should().NotBeNullOrWhiteSpace();
        jwtTokenDto.RefreshToken.ExpiresIn.Should().BeAfter(DateTime.Now);
    }


    [Fact]
    public void FindAuthenticatedUserId_Test()
    {
    }

    [Fact]
    public void GetJwtCookies_Test()
    {
        Guid userId = _faker.Random.Guid();

        JwtTokenDto jwtTokenDto = _authService.CreateJwtTokenData(userId);

        _authService.SendJwtCookiesToClient(jwtTokenDto);

        (string? accessToken, string? refreshToken) = _authService.GetJwtCookies();

        accessToken.Should().NotBeNullOrWhiteSpace();
        refreshToken.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public void SendJwtCookiesToClient_Test()
    {
    }

    [Fact]
    public void ValidateJwtTokenOrThrow_Test()
    {
    }
}

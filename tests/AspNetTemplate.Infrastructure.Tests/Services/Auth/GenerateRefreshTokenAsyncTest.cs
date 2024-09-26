// using FluentAssertions;

// using AspNetTemplate.Domain.Dtos;
// using AspNetTemplate.Domain.Tests.Common;
// using AspNetTemplate.Domain.Tests.Factories;

// namespace AspNetTemplate.Infrastructure.Tests.Services.Auth;

// public sealed class GenerateRefreshTokenAsyncTest(AspNetTemplateWebApplicationFactory factory) : BaseAuthTest(factory)
// {
//     [Fact]
//     public async Task ShouldGenerate_UniqueRefreshTokens()
//     {
//         UserDto userDto = new(_faker.Random.Guid(), _faker.Internet.Email());

//         string firstRefreshToken = await GenerateRefreshTokenAsync(userDto);
//         string secondRefreshToken = await GenerateRefreshTokenAsync(userDto);

//         firstRefreshToken.Should().NotBe(secondRefreshToken);
//     }

//     [Fact]
//     public async Task ShouldGenerate_ValidRefreshToken()
//     {
//         UserDto userDto = new(_faker.Random.Guid(), _faker.Internet.Email());

//         string refreshToken = await GenerateRefreshTokenAsync(userDto);

//         int expectedLength = 44;

//         refreshToken.Should().NotBeNullOrWhiteSpace();
//         refreshToken.Length.Should().Be(expectedLength);
//     }
// }

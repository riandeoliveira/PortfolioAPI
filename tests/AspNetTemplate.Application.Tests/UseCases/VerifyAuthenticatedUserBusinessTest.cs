// using System.Net;

// using FluentAssertions;

// using AspNetTemplate.Domain.Dtos;
// using AspNetTemplate.Domain.Tests.Common;
// using AspNetTemplate.Domain.Tests.Extensions;
// using AspNetTemplate.Domain.Tests.Factories;

// namespace AspNetTemplate.Application.Tests.UseCases.VerifyUser;

// public sealed class VerifyUserBusinessTest(AspNetTemplateWebApplicationFactory factory) : BaseAuthTest(factory)
// {
//     [Fact]
//     public async Task Should_VerifyUser()
//     {
//         await AuthenticateAsync();

//         UserDto currentUser = await GetCurrentUserAsync();

//         HttpResponseMessage response = await _client.GetAsync("/api/user");

//         UserDto body = await response.GetBodyAsync<UserDto>();

//         response.StatusCode.Should().Be(HttpStatusCode.OK);

//         currentUser.Should().NotBeNull();

//         currentUser.Id.Should().NotBe(Guid.Empty);
//         currentUser.Id.Should().Be(body.Id);

//         currentUser.Email.Should().NotBeNullOrWhiteSpace();
//         currentUser.Email.Should().Be(body.Email);
//     }

//     [Fact]
//     public async Task ShouldNot_VerifyUser_WithoutAuthentication()
//     {
//         HttpResponseMessage response = await _client.GetAsync("/api/user");

//         response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
//         response.ReasonPhrase.Should().Be("Unauthorized");
//     }
// }

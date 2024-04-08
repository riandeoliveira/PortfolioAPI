using System.Net;

using FluentAssertions;

using Portfolio.Domain.Dtos;
using Portfolio.Domain.Tests.Common;
using Portfolio.Domain.Tests.Extensions;
using Portfolio.Domain.Tests.Factories;

namespace Portfolio.Application.Tests.UseCases.FindCurrentUser;

public sealed class FindCurrentUserBusinessTest(PortfolioWebApplicationFactory factory) : BaseAuthTest(factory)
{
    [Fact]
    public async Task Should_FindCurrentUser()
    {
        await AuthenticateAsync();

        UserDto currentUser = await GetCurrentUserAsync();

        HttpResponseMessage response = await _client.GetAsync("/api/user");

        UserDto body = await response.GetBodyAsync<UserDto>();

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        currentUser.Should().NotBeNull();

        currentUser.Id.Should().NotBe(Guid.Empty);
        currentUser.Id.Should().Be(body.Id);

        currentUser.Email.Should().NotBeNullOrWhiteSpace();
        currentUser.Email.Should().Be(body.Email);
    }

    [Fact]
    public async Task ShouldNot_FindCurrentUser_WithoutAuthentication()
    {
        HttpResponseMessage response = await _client.GetAsync("/api/user");

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        response.ReasonPhrase.Should().Be("Unauthorized");
    }
}

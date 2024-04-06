using System.Net;

using FluentAssertions;

using Portfolio.Domain.Dtos;
using Portfolio.Domain.Tests.Common;
using Portfolio.Domain.Tests.Extensions;
using Portfolio.Domain.Tests.Factories;
using Portfolio.Domain.Tests.Helper;

namespace Portfolio.Application.Tests.UseCases.FindCurrentUser;

public sealed class FindCurrentUserBusinessTest(PortfolioWebApplicationFactory factory) : BaseTest(factory)
{
    [Fact]
    public async Task ShouldGetCurrentUser()
    {
        AuthHelper authHelper = new(_client);

        (_, TokenDto authBody) = await authHelper.AuthenticateAsync();

        UserDto currentUser = await AuthHelper.GetUserFromAccessTokenAsync(authBody.AccessToken);

        HttpResponseMessage response = await _client.GetAsync("/api/user");

        UserDto body = await response.GetBodyAsync<UserDto>();

        currentUser.Should().NotBeNull();

        currentUser.Id.Should().NotBe(Guid.Empty);

        currentUser.Id.Should().Be(body.Id);

        currentUser.Email.Should().NotBeNullOrWhiteSpace();
        currentUser.Email.Should().Be(body.Email);
    }

    [Fact]
    public async Task ShouldNotAccessWithoutAuthentication()
    {
        HttpResponseMessage response = await _client.GetAsync("/api/user");

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        response.ReasonPhrase.Should().Be("Unauthorized");
    }
}

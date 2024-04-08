using System.Net;

using FluentAssertions;

using Portfolio.Application.UseCases.CreateAuthor;
using Portfolio.Domain.Dtos;
using Portfolio.Domain.Tests.Common;
using Portfolio.Domain.Tests.Extensions;
using Portfolio.Domain.Tests.Factories;
using Portfolio.Domain.Tests.Fixtures;

namespace Portfolio.Application.Tests.UseCases.CreateAuthor;

public sealed class CreateAuthorBusinessTest(PortfolioWebApplicationFactory factory) : BaseAuthTest(factory)
{
    private static CreateAuthorRequest Request =>
        new(
            Name: DatabaseFixture.Author_1.Name,
            FullName: DatabaseFixture.Author_1.FullName,
            Position: DatabaseFixture.Author_1.Position,
            Description: DatabaseFixture.Author_1.Description,
            AvatarUrl: DatabaseFixture.Author_1.AvatarUrl,
            SpotifyAccountName: DatabaseFixture.Author_1.SpotifyAccountName
        );

    [Fact]
    public async Task Should_CreateAuthor()
    {
        await AuthenticateAsync();

        HttpResponseMessage response = await _client.SendPostAsync("/api/author", Request);

        AuthorDto body = await response.GetBodyAsync<AuthorDto>();

        bool authorExists = await _authorRepository.ExistAsync(body.Id);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        body.Should().NotBeNull();

        body.Id.Should().NotBe(Guid.Empty);

        body.Name.Should().NotBeNullOrWhiteSpace();
        body.Name.Should().Be(Request.Name);

        body.FullName.Should().NotBeNullOrWhiteSpace();
        body.FullName.Should().Be(Request.FullName);

        body.Position.Should().NotBeNullOrWhiteSpace();
        body.Position.Should().Be(Request.Position);

        body.Description.Should().NotBeNullOrWhiteSpace();
        body.Description.Should().Be(Request.Description);

        body.AvatarUrl.Should().NotBeNullOrWhiteSpace();
        body.AvatarUrl.Should().Be(Request.AvatarUrl);

        body.SpotifyAccountName.Should().Be(Request.SpotifyAccountName);

        authorExists.Should().BeTrue();
    }

    [Fact]
    public async Task ShouldNot_CreateAuthor_WithoutAuthentication()
    {
        HttpResponseMessage response = await _client.SendPostAsync("/api/author", Request);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        response.ReasonPhrase.Should().Be("Unauthorized");
    }
}

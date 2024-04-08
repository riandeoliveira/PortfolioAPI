using System.Net;

using FluentAssertions;

using Portfolio.Domain.Dtos;
using Portfolio.Domain.Tests.Common;
using Portfolio.Domain.Tests.Extensions;
using Portfolio.Domain.Tests.Factories;
using Portfolio.Domain.Tests.Fixtures;
using Portfolio.Domain.Tests.Helper;

namespace Portfolio.Application.Tests.UseCases.FindOneAuthor;

public sealed class FindOneAuthorBusinessTest : BaseTest
{
    public FindOneAuthorBusinessTest(PortfolioWebApplicationFactory factory) : base(factory)
        => new AuthHelper(_client).AuthenticateAsync().GetAwaiter().GetResult();

    [Fact]
    public async Task ShouldFindOneAuthor()
    {
        HttpResponseMessage response = await _client.GetAsync($"/api/author/{DatabaseFixture.Author.Id}");

        AuthorDto body = await response.GetBodyAsync<AuthorDto>();

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        body.Should().NotBeNull();

        body.Id.Should().NotBe(Guid.Empty);

        body.Name.Should().NotBeNullOrWhiteSpace();
        body.Name.Should().Be(DatabaseFixture.Author.Name);

        body.FullName.Should().NotBeNullOrWhiteSpace();
        body.FullName.Should().Be(DatabaseFixture.Author.FullName);

        body.Position.Should().NotBeNullOrWhiteSpace();
        body.Position.Should().Be(DatabaseFixture.Author.Position);

        body.Description.Should().NotBeNullOrWhiteSpace();
        body.Description.Should().Be(DatabaseFixture.Author.Description);

        body.AvatarUrl.Should().NotBeNullOrWhiteSpace();
        body.AvatarUrl.Should().Be(DatabaseFixture.Author.AvatarUrl);

        body.SpotifyAccountName.Should().Be(DatabaseFixture.Author.SpotifyAccountName);
    }

    [Fact]
    public async Task ShouldNotFindOneAuthorWithInvalidId()
    {
        Guid fakeId = _faker.Random.Guid();

        string expectedMessage = "Nenhum 'autor' encontrado.";

        HttpResponseMessage response = await _client.GetAsync($"/api/author/{fakeId}");

        string responseMessage = await response.Content.ReadAsStringAsync();
        string message = responseMessage.Trim('"');

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        message.Should().Be(expectedMessage);
    }
}

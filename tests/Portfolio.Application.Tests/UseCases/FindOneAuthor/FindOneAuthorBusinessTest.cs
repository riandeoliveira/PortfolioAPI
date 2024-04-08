using System.Net;

using FluentAssertions;

using Portfolio.Domain.Dtos;
using Portfolio.Domain.Tests.Common;
using Portfolio.Domain.Tests.Extensions;
using Portfolio.Domain.Tests.Factories;
using Portfolio.Domain.Tests.Fixtures;

namespace Portfolio.Application.Tests.UseCases.FindOneAuthor;

public sealed class FindOneAuthorBusinessTest(PortfolioWebApplicationFactory factory) : BaseAuthTest(factory)
{
    [Fact]
    public async Task Should_FindOneAuthor()
    {
        await AuthenticateAsync();

        HttpResponseMessage response = await _client.GetAsync($"/api/author/{DatabaseFixture.Author_1.Id}");

        AuthorDto body = await response.GetBodyAsync<AuthorDto>();

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        body.Should().NotBeNull();

        body.Id.Should().NotBe(Guid.Empty);

        body.Name.Should().NotBeNullOrWhiteSpace();
        body.Name.Should().Be(DatabaseFixture.Author_1.Name);

        body.FullName.Should().NotBeNullOrWhiteSpace();
        body.FullName.Should().Be(DatabaseFixture.Author_1.FullName);

        body.Position.Should().NotBeNullOrWhiteSpace();
        body.Position.Should().Be(DatabaseFixture.Author_1.Position);

        body.Description.Should().NotBeNullOrWhiteSpace();
        body.Description.Should().Be(DatabaseFixture.Author_1.Description);

        body.AvatarUrl.Should().NotBeNullOrWhiteSpace();
        body.AvatarUrl.Should().Be(DatabaseFixture.Author_1.AvatarUrl);

        body.SpotifyAccountName.Should().Be(DatabaseFixture.Author_1.SpotifyAccountName);
    }

    [Fact]
    public async Task ShouldNot_FindOneAuthor_FromAnotherUser()
    {
        await AuthenticateAsync();

        Guid idFromAnotherUser = DatabaseFixture.Author_2.Id;

        HttpResponseMessage response = await _client.GetAsync($"/api/author/{idFromAnotherUser}");

        string responseMessage = await response.Content.ReadAsStringAsync();
        string message = responseMessage.Trim('"');
        string expectedMessage = "Nenhum 'autor' encontrado.";

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        message.Should().Be(expectedMessage);
    }

    [Fact]
    public async Task ShouldNot_FindOneAuthor_WithInvalidId()
    {
        await AuthenticateAsync();

        Guid invalidId = _faker.Random.Guid();

        HttpResponseMessage response = await _client.GetAsync($"/api/author/{invalidId}");

        string responseMessage = await response.Content.ReadAsStringAsync();
        string message = responseMessage.Trim('"');
        string expectedMessage = "Nenhum 'autor' encontrado.";

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        message.Should().Be(expectedMessage);
    }

    [Fact]
    public async Task ShouldNot_FindOneAuthor_WithoutAuthentication()
    {
        Guid invalidId = _faker.Random.Guid();

        HttpResponseMessage response = await _client.GetAsync($"/api/author/{invalidId}");

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        response.ReasonPhrase.Should().Be("Unauthorized");
    }
}

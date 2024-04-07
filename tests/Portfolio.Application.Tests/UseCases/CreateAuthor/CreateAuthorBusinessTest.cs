using System.Net;

using FluentAssertions;

using Portfolio.Application.UseCases.CreateAuthor;
using Portfolio.Domain.Dtos;
using Portfolio.Domain.Tests.Common;
using Portfolio.Domain.Tests.Extensions;
using Portfolio.Domain.Tests.Factories;
using Portfolio.Domain.Tests.Helper;

namespace Portfolio.Application.Tests.UseCases.CreateAuthor;

public sealed class CreateAuthorBusinessTest : BaseTest
{
    public CreateAuthorBusinessTest(PortfolioWebApplicationFactory factory) : base(factory)
        => new AuthHelper(_client).AuthenticateAsync().GetAwaiter().GetResult();

    [Fact]
    public async Task ShouldCreateAuthor()
    {
        CreateAuthorRequest request = new(
            Name: _faker.Name.FirstName(),
            FullName: _faker.Name.FullName(),
            Position: _faker.Name.JobTitle(),
            Description: _faker.Lorem.Sentence(),
            AvatarUrl: _faker.Internet.Url(),
            SpotifyAccountName: _faker.Internet.UserName()
        );

        HttpResponseMessage response = await _client.SendPostAsync("/api/author", request);

        AuthorDto body = await response.GetBodyAsync<AuthorDto>();

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        body.Should().NotBeNull();

        body.Id.Should().NotBe(Guid.Empty);

        body.Name.Should().NotBeNullOrWhiteSpace();
        body.Name.Should().Be(request.Name);

        body.FullName.Should().NotBeNullOrWhiteSpace();
        body.FullName.Should().Be(request.FullName);

        body.Position.Should().NotBeNullOrWhiteSpace();
        body.Position.Should().Be(request.Position);

        body.Description.Should().NotBeNullOrWhiteSpace();
        body.Description.Should().Be(request.Description);

        body.AvatarUrl.Should().NotBeNullOrWhiteSpace();
        body.AvatarUrl.Should().Be(request.AvatarUrl);
    }
}

using System.Net;

using FluentAssertions;

using Portfolio.Application.UseCases.CreateAuthor;
using Portfolio.Domain.Tests.Common;
using Portfolio.Domain.Tests.Extensions;
using Portfolio.Domain.Tests.Factories;
using Portfolio.Domain.Tests.Helper;

namespace Portfolio.Application.Tests.UseCases.CreateAuthor;

public sealed class CreateAuthorValidatorTest(PortfolioWebApplicationFactory factory) : BaseTest(factory)
{
    [Fact]
    public async Task NameValidationTest()
    {
        AuthHelper authHelper = new(_client);

        await authHelper.AuthenticateAsync();

        CreateAuthorRequest request = new(
            Name: _faker.Name.FirstName(),
            FullName: _faker.Name.FullName(),
            Position: _faker.Name.JobTitle(),
            Description: _faker.Lorem.Sentence(),
            AvatarUrl: _faker.Internet.Url(),
            SpotifyAccountName: _faker.Internet.UserName()
        );

        HttpResponseMessage response = await _client.SendPostAsync("/api/author", request);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}

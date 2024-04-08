using System.Net;

using FluentAssertions;

using Portfolio.Application.UseCases.SignInUser;
using Portfolio.Domain.Tests.Common;
using Portfolio.Domain.Tests.Extensions;
using Portfolio.Domain.Tests.Factories;
using Portfolio.Domain.Tests.Fixtures;
using Portfolio.Infrastructure.Extensions;

namespace Portfolio.Application.Tests.UseCases.SignInUser;

public sealed class SignInUserBusinessTest(PortfolioWebApplicationFactory factory) : BaseAuthTest(factory)
{
    [Fact]
    public async Task Should_SignInUser()
    {
        SignInUserRequest signInRequest = new(DatabaseFixture.User_1.Email, DatabaseFixture.User_1.Password);

        HttpResponseMessage response = await _client.SendPostAsync("/api/user/sign-in", signInRequest);

        response.Should().HaveStatusCode(HttpStatusCode.OK);
    }

    [Fact]
    public async Task ShouldNot_SignInUser_WithUnregisteredEmail()
    {
        SignInUserRequest request = new(
            _faker.Internet.Email(),
            _faker.Internet.StrongPassword()
        );

        HttpResponseMessage response = await _client.SendPostAsync("/api/user/sign-in", request);

        string responseMessage = await response.Content.ReadAsStringAsync();
        string message = responseMessage.Trim('"');
        string expectedMessage = "Este 'e-mail' não está registrado.";

        response.Should().HaveStatusCode(HttpStatusCode.BadRequest);

        message.Should().Be(expectedMessage);
    }
}

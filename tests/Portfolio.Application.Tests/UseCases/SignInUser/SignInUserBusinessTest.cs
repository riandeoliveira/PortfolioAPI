using System.Net;

using FluentAssertions;

using Portfolio.Application.UseCases.SignInUser;
using Portfolio.Application.UseCases.SignUpUser;
using Portfolio.Domain.Tests.Common;
using Portfolio.Domain.Tests.Extensions;
using Portfolio.Domain.Tests.Factories;
using Portfolio.Domain.Tests.Helper;
using Portfolio.Infrastructure.Extensions;

namespace Portfolio.Application.Tests.UseCases.SignInUser;

public sealed class SignInUserBusinessTest(PortfolioWebApplicationFactory factory) : BaseTest(factory)
{
    [Fact]
    public async Task ShouldNotSignInUserWithUnregisteredEmail()
    {
        string expectedMessage = "Este 'e-mail' não está registrado.";

        SignInUserRequest request = new(
            _faker.Internet.Email(),
            _faker.Internet.StrongPassword()
        );

        HttpResponseMessage response = await _client.SendPostAsync("/api/user/sign-in", request);

        string responseMessage = await response.Content.ReadAsStringAsync();
        string message = responseMessage.Trim('"');

        response.Should().HaveStatusCode(HttpStatusCode.BadRequest);

        message.Should().Be(expectedMessage);
    }

    [Fact]
    public async Task ShouldSignInUser()
    {
        AuthHelper authHelper = new(_client);

        (SignUpUserRequest signUpRequest, _) = await authHelper.AuthenticateAsync();

        SignInUserRequest signInRequest = new(signUpRequest.Email, signUpRequest.Password);

        HttpResponseMessage response = await _client.SendPostAsync("/api/user/sign-in", signInRequest);

        response.Should().HaveStatusCode(HttpStatusCode.OK);
    }
}

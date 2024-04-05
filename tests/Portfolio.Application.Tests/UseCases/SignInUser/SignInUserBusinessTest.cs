using System.Net;
using System.Net.Http.Json;
using System.Reflection;

using FluentAssertions;

using Portfolio.Application.UseCases.SignInUser;
using Portfolio.Application.UseCases.SignUpUser;
using Portfolio.Domain.Tests.Common;
using Portfolio.Domain.Tests.Factories;

namespace Portfolio.Application.Tests.UseCases.SignInUser;

public sealed class SignInUserBusinessTest(PortfolioWebApplicationFactory factory) : BaseTest(factory)
{
    [Fact]
    public async Task ShouldAuthenticateUser()
    {
        SignUpUserRequest signUpRequest = await AuthenticateAsync();

        SignInUserRequest signInRequest = new(signUpRequest.Email, signUpRequest.Password);

        HttpResponseMessage response = await _client.PostAsJsonAsync("/api/user/sign-in", signInRequest);

        response.Should().HaveStatusCode(HttpStatusCode.OK);
    }

    [Fact]
    public async Task ShouldNotUseUnregisteredEmail()
    {
        string email = _faker.Internet.Email();
        string password = _faker.Internet.Password();
        string expectedMessage = "Este 'e-mail' não está registrado.";

        SignInUserRequest request = new(email, password);
        HttpResponseMessage response = await _client.PostAsJsonAsync("/api/user/sign-in", request);

        string responseMessage = await response.Content.ReadAsStringAsync();
        string message = responseMessage.Trim('"');

        response.Should().HaveStatusCode(HttpStatusCode.BadRequest);

        message.Should().Be(expectedMessage);
    }
}

using System.Net;
using System.Net.Http.Json;

using FluentAssertions;

using Portfolio.Application.UseCases.SignInUser;
using Portfolio.Application.UseCases.SignUpUser;
using Portfolio.Domain.Tests.Common;
using Portfolio.Domain.Tests.Factories;
using Portfolio.Domain.Tests.Helper;

using Portfolio.Infrastructure.Extensions;

namespace Portfolio.Application.Tests.UseCases.SignInUser;

public sealed class SignInUserValidatorTest(PortfolioWebApplicationFactory factory) : BaseTest(factory)
{
    [InlineData("", "O 'e-mail' deve ser informado.")]
    [InlineData("john", "O 'e-mail' deve possuir no mínimo 8 caracteres.")]
    [InlineData("joooooooooooooooooooooooooooooooooooooooooooooooohn2000@email.com", "O 'e-mail' deve possuir no máximo 64 caracteres.")]
    [InlineData("john@test", "O 'e-mail' deve ser válido.")]
    [Theory]
    public async Task EmailValidationTest(string email, string expectedMessage)
    {
        SignInUserRequest request = new(
            Email: email,
            Password: _faker.Internet.StrongPassword()
        );

        HttpResponseMessage response = await _client.PostAsJsonAsync("/api/user/sign-in", request);

        string message = await response.Content.ReadAsStringAsync();

        response.Should().HaveStatusCode(HttpStatusCode.BadRequest);

        message.Trim('"').Should().Be(expectedMessage);
    }

    [InlineData("", "A 'senha' deve ser informada.")]
    [InlineData("little", "A 'senha' deve possuir no mínimo 8 caracteres.")]
    [InlineData("littlejoooooooooooooooooooooooooooooooooooooooooooooooooooohn2000", "A 'senha' deve possuir no máximo 64 caracteres.")]
    [InlineData("littlejohn", "A 'senha' deve possuir pelo menos: uma letra maiúscula e um número.")]
    [Theory]
    public async Task PasswordValidationTest(string password, string expectedMessage)
    {
        AuthHelper authHelper = new(_client);

        (SignUpUserRequest signUpRequest, _) = await authHelper.AuthenticateAsync();

        SignInUserRequest signInRequest = new(
            Email: signUpRequest.Email,
            Password: password
        );

        HttpResponseMessage response = await _client.PostAsJsonAsync("/api/user/sign-in", signInRequest);

        string message = await response.Content.ReadAsStringAsync();

        response.Should().HaveStatusCode(HttpStatusCode.BadRequest);

        message.Trim('"').Should().Be(expectedMessage);
    }
}

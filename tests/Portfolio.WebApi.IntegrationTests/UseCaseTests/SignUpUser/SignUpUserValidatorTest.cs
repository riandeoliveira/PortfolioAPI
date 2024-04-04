using System.Net;
using System.Net.Http.Json;

using FluentAssertions;

using Portfolio.Application.UseCases.SignUpUser;
using Portfolio.WebApi.IntegrationTests.Common;
using Portfolio.WebApi.IntegrationTests.Factories;

namespace Portfolio.WebApi.IntegrationTests.UseCaseTests.SignUpUser;

public sealed class SignUpUserValidatorTest(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [InlineData("", "O 'e-mail' deve ser informado.")]
    [InlineData("john", "O 'e-mail' deve possuir no mínimo 8 caracteres.")]
    [InlineData("joooooooooooooooooooooooooooooooooooooooooooooooohn2000@email.com", "O 'e-mail' deve possuir no máximo 64 caracteres.")]
    [InlineData("john@test", "O 'e-mail' deve ser válido.")]
    [Theory]
    public async Task EmailValidationTest(string email, string expectedMessage)
    {
        SignUpUserRequest request = new(
            Email: email,
            Password: "LittleJohn2000"
        );

        HttpResponseMessage response = await _client.PostAsJsonAsync("/api/user/sign-up", request);
        string message = await response.Content.ReadAsStringAsync();

        response.Should().HaveStatusCode(HttpStatusCode.BadRequest);
        message.Trim('"').Should().BeEquivalentTo(expectedMessage);
    }

    [InlineData("", "A 'senha' deve ser informada.")]
    [InlineData("little", "A 'senha' deve possuir no mínimo 8 caracteres.")]
    [InlineData("littlejoooooooooooooooooooooooooooooooooooooooooooooooooooohn2000", "A 'senha' deve possuir no máximo 64 caracteres.")]
    [InlineData("littlejohn", "A 'senha' deve possuir pelo menos: uma letra maiúscula e um número.")]
    [Theory]
    public async Task PasswordValidationTest(string password, string expectedMessage)
    {
        SignUpUserRequest request = new(
            Email: "johndoe2000@email.com",
            Password: password
        );

        HttpResponseMessage response = await _client.PostAsJsonAsync("/api/user/sign-up", request);
        string message = await response.Content.ReadAsStringAsync();

        response.Should().HaveStatusCode(HttpStatusCode.BadRequest);
        message.Trim('"').Should().BeEquivalentTo(expectedMessage);
    }
}

using Portfolio.Application.UseCases.SignUpUser;
using Portfolio.Domain.Tests.Common;
using Portfolio.Domain.Tests.Factories;

using Portfolio.Infrastructure.Extensions;

namespace Portfolio.Application.Tests.UseCases.SignUpUser;

public sealed class SignUpUserValidatorTest(PortfolioWebApplicationFactory factory) : BaseValidationTest(factory)
{
    public SignUpUserRequest CreateRequest(string? email = null, string? password = null) =>
        new(
            email ?? _faker.Internet.Email(),
            password ?? _faker.Internet.StrongPassword()
        );

    [InlineData(EMPTY_STRING, "O 'e-mail' deve ser informado.")]
    [InlineData(STRING_WITH_SIZE_7, "O 'e-mail' deve possuir no mínimo 8 caracteres.")]
    [InlineData(STRING_WITH_SIZE_65, "O 'e-mail' deve possuir no máximo 64 caracteres.")]
    [InlineData(INVALID_EMAIL, "O 'e-mail' deve ser válido.")]
    [Theory]
    public async Task Email_ValidationTest(string email, string expectedMessage) =>
        await ExecuteAsync("/api/user/sign-up", CreateRequest(email: email), expectedMessage, false);

    [InlineData(EMPTY_STRING, "A 'senha' deve ser informada.")]
    [InlineData(STRING_WITH_SIZE_7, "A 'senha' deve possuir no mínimo 8 caracteres.")]
    [InlineData(STRING_WITH_SIZE_65, "A 'senha' deve possuir no máximo 64 caracteres.")]
    [InlineData(WEAK_PASSWORD, "A 'senha' deve possuir pelo menos: uma letra maiúscula e um número.")]
    [Theory]
    public async Task Password_ValidationTest(string password, string expectedMessage) =>
        await ExecuteAsync("/api/user/sign-up", CreateRequest(password: password), expectedMessage, false);
}

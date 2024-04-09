using Portfolio.Application.UseCases.SignInUser;
using Portfolio.Domain.Messages;
using Portfolio.Domain.Tests.Common;
using Portfolio.Domain.Tests.Factories;
using Portfolio.Domain.Tests.Fixtures;

using Portfolio.Infrastructure.Extensions;

namespace Portfolio.Application.Tests.UseCases.SignInUser;

public sealed class SignInUserValidatorTest(PortfolioWebApplicationFactory factory) : BaseValidationTest(factory)
{
    public SignInUserRequest CreateRequest(string? email = null, string? password = null) =>
        new(
            email ?? _faker.Internet.Email(),
            password ?? _faker.Internet.StrongPassword()
        );

    [InlineData(EMPTY_STRING, Messages_PT_BR.EmailIsRequired)]
    [InlineData(STRING_WITH_SIZE_7, Messages_PT_BR.MinimumEmailLength)]
    [InlineData(STRING_WITH_SIZE_65, Messages_PT_BR.MaximumEmailLength)]
    [InlineData(INVALID_EMAIL, Messages_PT_BR.InvalidEmail)]
    [Theory]
    public async Task Email_ValidationTest(string email, string expectedMessage) =>
        await PostAsync("/api/user/sign-in", CreateRequest(email: email), expectedMessage, false);

    [InlineData(EMPTY_STRING, Messages_PT_BR.PasswordIsRequired)]
    [InlineData(STRING_WITH_SIZE_7, Messages_PT_BR.MinimumPasswordLength)]
    [InlineData(STRING_WITH_SIZE_65, Messages_PT_BR.MaximumPasswordLength)]
    [InlineData(WEAK_PASSWORD, Messages_PT_BR.StrongPassword)]
    [Theory]
    public async Task Password_ValidationTest(string password, string expectedMessage)
    {
        SignInUserRequest signInRequest = new(
            Email: DatabaseFixture.User_1.Email,
            Password: password
        );

        await PostAsync("/api/user/sign-in", signInRequest, expectedMessage, false);
    }
}

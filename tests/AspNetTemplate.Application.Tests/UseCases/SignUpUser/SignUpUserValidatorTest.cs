using AspNetTemplate.Application.UseCases.SignUpUser;
using AspNetTemplate.Domain.Messages;
using AspNetTemplate.Domain.Tests.Common;
using AspNetTemplate.Domain.Tests.Factories;

using AspNetTemplate.Infrastructure.Extensions;

namespace AspNetTemplate.Application.Tests.UseCases.SignUpUser;

public sealed class SignUpUserValidatorTest(AspNetTemplateWebApplicationFactory factory) : BaseValidationTest(factory)
{
    public SignUpUserRequest CreateRequest(string? email = null, string? password = null) =>
        new(
            email ?? _faker.Internet.Email(),
            password ?? _faker.Internet.StrongPassword()
        );

    [InlineData(EMPTY_STRING, Messages_PT_BR.EmailIsRequired)]
    [InlineData(STRING_WITH_SIZE_7, Messages_PT_BR.MinimumEmailLength)]
    [InlineData(STRING_WITH_SIZE_65, Messages_PT_BR.MaximumEmailLength)]
    [InlineData(INVALID_EMAIL, Messages_PT_BR.EmailIsValid)]
    [Theory]
    public async Task Email_ValidationTest(string email, string expectedMessage) =>
        await PostAsync("/api/user/sign-up", CreateRequest(email: email), expectedMessage, false);

    [InlineData(EMPTY_STRING, Messages_PT_BR.PasswordIsRequired)]
    [InlineData(STRING_WITH_SIZE_7, Messages_PT_BR.MinimumPasswordLength)]
    [InlineData(STRING_WITH_SIZE_65, Messages_PT_BR.MaximumPasswordLength)]
    [InlineData(WEAK_PASSWORD, Messages_PT_BR.StrongPassword)]
    [Theory]
    public async Task Password_ValidationTest(string password, string expectedMessage) =>
        await PostAsync("/api/user/sign-up", CreateRequest(password: password), expectedMessage, false);
}

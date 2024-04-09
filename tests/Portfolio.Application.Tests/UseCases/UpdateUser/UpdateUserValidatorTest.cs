using Portfolio.Application.UseCases.UpdateUser;
using Portfolio.Domain.Messages;
using Portfolio.Domain.Tests.Common;
using Portfolio.Domain.Tests.Factories;
using Portfolio.Domain.Tests.Fixtures;

namespace Portfolio.Application.Tests.UseCases.UpdateUser;

public sealed class UpdateUserValidatorTest(PortfolioWebApplicationFactory factory) : BaseValidationTest(factory)
{
    public static UpdateUserRequest CreateRequest(string? email = null, string? password = null) =>
        new(
            email ?? DatabaseFixture.User_1.Email,
            password ?? DatabaseFixture.User_1.Password
        );

    [InlineData(EMPTY_STRING, Messages_PT_BR.EmailIsRequired)]
    [InlineData(STRING_WITH_SIZE_7, Messages_PT_BR.MinimumEmailLength)]
    [InlineData(STRING_WITH_SIZE_65, Messages_PT_BR.MaximumEmailLength)]
    [InlineData(INVALID_EMAIL, Messages_PT_BR.InvalidEmail)]
    [Theory]
    public async Task Email_ValidationTest(string email, string expectedMessage) =>
        await PutAsync("/api/user", CreateRequest(email: email), expectedMessage);

    [InlineData(EMPTY_STRING, Messages_PT_BR.PasswordIsRequired)]
    [InlineData(STRING_WITH_SIZE_7, Messages_PT_BR.MinimumPasswordLength)]
    [InlineData(STRING_WITH_SIZE_65, Messages_PT_BR.MaximumPasswordLength)]
    [InlineData(WEAK_PASSWORD, Messages_PT_BR.StrongPassword)]
    [Theory]
    public async Task Password_ValidationTest(string password, string expectedMessage) =>
        await PutAsync("/api/user", CreateRequest(password: password), expectedMessage);
}

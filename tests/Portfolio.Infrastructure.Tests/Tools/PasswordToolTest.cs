using Bogus;

using FluentAssertions;

using Portfolio.Infrastructure.Tools;

using Portolio.Infrastructure.Extensions;

namespace Portfolio.Infrastructure.Tests.Tools;

public sealed class PasswordToolTest
{
    private readonly Faker _faker = new();

    [Fact]
    public void ShouldBeFalseForIncorrectPassword()
    {
        string password = _faker.Internet.StrongPassword();
        string hashedPassword = PasswordTool.Hash(password);
        string incorrectPassword = _faker.Internet.StrongPassword();

        bool isValidPassword = PasswordTool.Verify(incorrectPassword, hashedPassword);

        isValidPassword.Should().BeFalse();
    }

    [Fact]
    public void ShouldBeTrueForCorrectPassword()
    {
        string password = _faker.Internet.StrongPassword();
        string hashedPassword = PasswordTool.Hash(password);

        bool isValidPassword = PasswordTool.Verify(password, hashedPassword);

        isValidPassword.Should().BeTrue();
    }
}

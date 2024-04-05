using Bogus;

using FluentAssertions;

using Portfolio.Domain.Tests.Common;
using Portfolio.Domain.Tests.Factories;
using Portfolio.Infrastructure.Tools;

using Portolio.Infrastructure.Extensions;

namespace Portfolio.Infrastructure.Tests.Tools;

public sealed class PasswordToolTest(PortfolioWebApplicationFactory factory) : BaseTest(factory)
{
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

    [Fact]
    public void ShouldGenerateUniqueHashes()
    {
        string password = _faker.Internet.StrongPassword();
        string firstHashedPassword = PasswordTool.Hash(password);
        string secondHashedPassword = PasswordTool.Hash(password);

        firstHashedPassword.Should().NotBe(secondHashedPassword);
    }
}

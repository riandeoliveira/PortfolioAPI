using FluentAssertions;

using AspNetTemplate.Domain.Tests.Common;
using AspNetTemplate.Domain.Tests.Factories;
using AspNetTemplate.Infrastructure.Tools;

using AspNetTemplate.Infrastructure.Extensions;

namespace AspNetTemplate.Infrastructure.Tests.Tools;

public sealed class PasswordToolTest(AspNetTemplateWebApplicationFactory factory) : BaseTest(factory)
{
    [Fact]
    public void ShouldBeFalse_ForIncorrectPassword()
    {
        string password = _faker.Internet.StrongPassword();
        string hashedPassword = PasswordTool.Hash(password);
        string incorrectPassword = _faker.Internet.StrongPassword();

        bool isValidPassword = PasswordTool.Verify(incorrectPassword, hashedPassword);

        isValidPassword.Should().BeFalse();
    }

    [Fact]
    public void ShouldBeTrue_ForCorrectPassword()
    {
        string password = _faker.Internet.StrongPassword();
        string hashedPassword = PasswordTool.Hash(password);

        bool isValidPassword = PasswordTool.Verify(password, hashedPassword);

        isValidPassword.Should().BeTrue();
    }

    [Fact]
    public void ShouldGenerate_UniqueHashes()
    {
        string password = _faker.Internet.StrongPassword();
        string firstHashedPassword = PasswordTool.Hash(password);
        string secondHashedPassword = PasswordTool.Hash(password);

        firstHashedPassword.Should().NotBe(secondHashedPassword);
    }
}

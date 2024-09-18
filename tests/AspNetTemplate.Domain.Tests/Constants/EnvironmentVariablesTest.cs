using FluentAssertions;

using AspNetTemplate.Domain.Constants;
using AspNetTemplate.Domain.Tests.Common;
using AspNetTemplate.Domain.Tests.Factories;

namespace AspNetTemplate.Domain.Tests.Constants;

public sealed class EnvironmentVariablesTest(AspNetTemplateWebApplicationFactory factory) : BaseTest(factory)
{
    [Fact]
    public static void ShouldHaveValidEnvironmentVariables()
    {
        EnvironmentVariables.CLIENT_URL.Should().NotBeNullOrWhiteSpace();

        EnvironmentVariables.DATABASE_HOST.Should().NotBeNullOrWhiteSpace();
        EnvironmentVariables.DATABASE_NAME.Should().NotBeNullOrWhiteSpace();
        EnvironmentVariables.DATABASE_PASSWORD.Should().NotBeNullOrWhiteSpace();
        EnvironmentVariables.DATABASE_PORT.Should().NotBeNullOrWhiteSpace();
        EnvironmentVariables.DATABASE_USER.Should().NotBeNullOrWhiteSpace();

        EnvironmentVariables.MAIL_HOST.Should().NotBeNullOrWhiteSpace();
        EnvironmentVariables.MAIL_PASSWORD.Should().NotBeNullOrWhiteSpace();
        EnvironmentVariables.MAIL_PORT.Should().NotBeNullOrWhiteSpace();
        EnvironmentVariables.MAIL_SENDER.Should().NotBeNullOrWhiteSpace();
        EnvironmentVariables.MAIL_USERNAME.Should().NotBeNullOrWhiteSpace();

        EnvironmentVariables.JWT_AUDIENCE.Should().NotBeNullOrWhiteSpace();
        EnvironmentVariables.JWT_ISSUER.Should().NotBeNullOrWhiteSpace();
        EnvironmentVariables.JWT_SECRET.Should().NotBeNullOrWhiteSpace();
    }
}

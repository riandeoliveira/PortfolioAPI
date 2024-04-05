using FluentAssertions;

using Portfolio.Domain.Constants;
using Portfolio.Domain.Tests.Common;
using Portfolio.Domain.Tests.Factories;

namespace Portfolio.Domain.Tests.Constants;

public sealed class EnvironmentVariablesTest(PortfolioWebApplicationFactory factory) : BaseTest(factory)
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

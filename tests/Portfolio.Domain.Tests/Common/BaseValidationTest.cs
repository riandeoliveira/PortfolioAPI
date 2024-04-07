using System.Net;

using FluentAssertions;

using Portfolio.Domain.Tests.Extensions;
using Portfolio.Domain.Tests.Factories;
using Portfolio.Domain.Tests.Helper;

namespace Portfolio.Domain.Tests.Common;

public abstract class BaseValidationTest(PortfolioWebApplicationFactory factory) : BaseTest(factory)
{
    public const string EMPTY_STRING = "";

    public const string INVALID_EMAIL = "john@test";

    public const string STRING_WITH_SIZE_7 = "*******";

    public const string STRING_WITH_SIZE_65 = "*****************************************************************";

    public const string STRING_WITH_SIZE_129 = "*********************************************************************************************************************************";

    public const string STRING_WITH_SIZE_513 = "*********************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************";

    public const string STRING_WITH_SIZE_1025 = "*****************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************";

    public const string WEAK_PASSWORD = "littlejohn";

    public async Task ExecuteValidationTestAsync<TRequest>(
        string requestUri,
        TRequest request,
        string expectedMessage,
        bool requireAuthentication = true
    )
    {
        if (requireAuthentication)
        {
            AuthHelper authHelper = new(_client);

            await authHelper.AuthenticateAsync();
        }

        HttpResponseMessage response = await _client.SendPostAsync(requestUri, request);

        string message = await response.Content.ReadAsStringAsync();

        response.Should().HaveStatusCode(HttpStatusCode.BadRequest);

        message.Trim('"').Should().Be(expectedMessage);
    }
}

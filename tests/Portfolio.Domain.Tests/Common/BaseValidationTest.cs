using System.Net;

using FluentAssertions;

using Portfolio.Domain.Tests.Extensions;
using Portfolio.Domain.Tests.Factories;

namespace Portfolio.Domain.Tests.Common;

public abstract class BaseValidationTest(PortfolioWebApplicationFactory factory) : BaseAuthTest(factory)
{
    public const string EMPTY_STRING = "";

    public const string INVALID_EMAIL = "john@test";

    public const string STRING_WITH_SIZE_7 = "*******";

    public const string STRING_WITH_SIZE_65 = "*****************************************************************";

    public const string STRING_WITH_SIZE_129 = "*********************************************************************************************************************************";

    public const string STRING_WITH_SIZE_513 = "*********************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************";

    public const string STRING_WITH_SIZE_1025 = "*****************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************";

    public const string WEAK_PASSWORD = "littlejohn";

    private static async Task ExecuteAsync(HttpResponseMessage response, string expectedMessage)
    {
        string message = await response.Content.ReadAsStringAsync();

        response.Should().HaveStatusCode(HttpStatusCode.BadRequest);

        message.Trim('"').Should().Be(expectedMessage);
    }

    public async Task PostAsync<TRequest>(
        string requestUri,
        TRequest request,
        string expectedMessage,
        bool requireAuthentication = true
    )
    {
        if (requireAuthentication) await AuthenticateAsync();

        HttpResponseMessage response = await _client.SendPostAsync(requestUri, request);

        await ExecuteAsync(response, expectedMessage);
    }

    public async Task PutAsync<TRequest>(
        string requestUri,
        TRequest request,
        string expectedMessage,
        bool requireAuthentication = true
    )
    {
        if (requireAuthentication) await AuthenticateAsync();

        HttpResponseMessage response = await _client.SendPutAsync(requestUri, request);

        await ExecuteAsync(response, expectedMessage);
    }
}

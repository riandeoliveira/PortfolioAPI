using System.Net;

using FluentAssertions;

using AspNetTemplate.Domain.Tests.Extensions;
using AspNetTemplate.Domain.Tests.Factories;

namespace AspNetTemplate.Domain.Tests.Common;

public abstract class BaseValidationTest(AspNetTemplateWebApplicationFactory factory) : BaseAuthTest(factory)
{
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

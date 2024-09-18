using System.Net.Http.Json;
using System.Text.Json;

namespace AspNetTemplate.Domain.Tests.Extensions;

public static class HttpClientExtension
{
    private static JsonSerializerOptions Options => new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    public static async Task<HttpResponseMessage> SendPostAsync<TRequest>(
        this HttpClient client,
        string requestUri,
        TRequest request
    ) => await client.PostAsJsonAsync(requestUri, request, Options);

    public static async Task<HttpResponseMessage> SendPutAsync<TRequest>(
        this HttpClient client,
        string requestUri,
        TRequest request
    ) => await client.PutAsJsonAsync(requestUri, request, Options);
}

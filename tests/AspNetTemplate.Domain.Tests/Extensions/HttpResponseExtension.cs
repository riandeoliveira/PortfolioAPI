using System.Net.Http.Json;
using System.Text.Json;

namespace AspNetTemplate.Domain.Tests.Extensions;

public static class HttpResponseExtension
{
    private static JsonSerializerOptions Options => new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    public static async Task<TBody> GetBodyAsync<TBody>(this HttpResponseMessage response)
    {
        TBody? body = await response.Content.ReadFromJsonAsync<TBody>(Options);

        return body ?? Activator.CreateInstance<TBody>();
    }
}

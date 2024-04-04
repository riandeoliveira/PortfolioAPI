using System.Net.Http.Json;
using System.Text.Json;

namespace Portfolio.WebApi.IntegrationTests.Extensions;

public static class HttpResponseExtensions
{
    public static async Task<TBody> GetBody<TBody>(this HttpResponseMessage response)
    {
        JsonSerializerOptions options = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        };

        TBody? body = await response.Content.ReadFromJsonAsync<TBody>(options);

        return body ?? Activator.CreateInstance<TBody>();
    }
}

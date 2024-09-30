using Microsoft.AspNetCore.RateLimiting;

namespace AspNetTemplate.Api.Extensions;

internal static class RateLimitExtension
{
    internal static WebApplicationBuilder ConfigureRateLimit(this WebApplicationBuilder builder)
    {
        builder.Services.AddRateLimiter(options => options.AddFixedWindowLimiter("Fixed", opt =>
            {
                opt.Window = TimeSpan.FromSeconds(60);
                opt.PermitLimit = 100;

                options.RejectionStatusCode = 429;
            }));

        return builder;
    }

    internal static WebApplication UseRateLimit(this WebApplication app)
    {
        app.UseRateLimiter();

        return app;
    }
}

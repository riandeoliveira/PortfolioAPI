using AspNetTemplate.Domain.Constants;

using HealthChecks.UI.Client;

using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace AspNetTemplate.Api.Extensions;

internal static class HealthCheckExtension
{
    internal static WebApplicationBuilder ConfigureHealthCheck(this WebApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks().AddNpgSql(DatabaseAccess.CONNECTION_STRING);

        return builder;
    }

    internal static WebApplication UseHealthCheck(this WebApplication app)
    {
        HealthCheckOptions options = new()
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        };

        app.MapHealthChecks("health", options);

        return app;
    }
}

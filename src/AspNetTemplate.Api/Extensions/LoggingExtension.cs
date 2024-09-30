using Serilog;

namespace AspNetTemplate.Api.Extensions;

internal static class LoggingExtension
{
    internal static WebApplicationBuilder ConfigureLogging(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, config) => config.ReadFrom.Configuration(context.Configuration));

        return builder;
    }

    internal static WebApplication UseLogging(this WebApplication app)
    {
        app.UseSerilogRequestLogging();

        return app;
    }
}

using AspNetTemplate.Api.Middlewares;

using FluentValidation;

using System.Reflection;
using System.Text.Json;

namespace AspNetTemplate.Api.Extensions;

internal static class HttpExtension
{
    internal static WebApplicationBuilder ConfigureHttp(this WebApplicationBuilder builder)
    {
        Assembly? applicationAssembly = Assembly.Load("AspNetTemplate.Application");

        builder.Services
            .AddValidatorsFromAssembly(applicationAssembly)
            .AddMediatR(config => config.RegisterServicesFromAssembly(applicationAssembly))
            .AddControllersWithViews()
            .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true)
            .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower);

        return builder;
    }

    internal static WebApplication UseHttp(this WebApplication app)
    {
        app.UseStatusCodePages();
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseMiddleware<JwtCookieMiddleware>();
        app.UseHttpsRedirection();
        app.MapControllers();

        return app;
    }
}

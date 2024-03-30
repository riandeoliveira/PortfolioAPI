using System.Text.Json;

using FluentValidation;

using Portfolio.Application.Endpoints;

using Serilog;

namespace Portfolio.WebApi.Extensions;

internal static class ControllerExtension
{
    internal static WebApplicationBuilder ConfigureControllers(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddValidatorsFromAssemblyContaining<BaseEndpoint>()
            .AddMediatR(configuration => configuration.RegisterServicesFromAssemblyContaining<BaseEndpoint>())
            .AddProblemDetails()
            .AddControllersWithViews()
            .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower);

        builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

        return builder;
    }
}

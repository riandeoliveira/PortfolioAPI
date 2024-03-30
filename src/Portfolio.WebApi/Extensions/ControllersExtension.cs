using System.Text.Json;

using FluentValidation;

using MediatR;

using Portfolio.Application.Endpoints;
using Portfolio.Infrastructure.Behaviors;

using Serilog;

namespace Portfolio.WebApi.Extensions;

internal static class ControllerExtension
{
    internal static WebApplicationBuilder ConfigureControllers(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ConversionBehavior<,>))
            .AddValidatorsFromAssemblyContaining<BaseEndpoint>()
            .AddProblemDetails()
            .AddControllersWithViews()
            .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower);

        builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

        return builder;
    }
}

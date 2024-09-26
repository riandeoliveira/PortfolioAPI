using System.Text.Json;

using FluentValidation;

using AspNetTemplate.Application.Endpoints;
using AspNetTemplate.Domain.Constants;

using Serilog;

namespace AspNetTemplate.WebApi.Extensions;

internal static class ControllerExtension
{
    internal static WebApplicationBuilder ConfigureControllers(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddValidatorsFromAssemblyContaining<BaseEndpoint>()
            .AddMediatR(configuration => configuration.RegisterServicesFromAssemblyContaining<BaseEndpoint>())
            .AddControllersWithViews()
            .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true)
            .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower);

        builder.Services.AddHealthChecks().AddNpgSql(Database.CONNECTION_STRING);

        builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

        return builder;
    }
}

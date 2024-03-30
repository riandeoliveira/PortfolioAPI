using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using Portfolio.Application.Endpoints;

using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Portfolio.WebApi.Extensions;

internal sealed class AddHeaderParameter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= [];

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "Accept-Language",
            Description = "The natural language and locale that the client prefers.",
            In = ParameterLocation.Header,
            Schema = new OpenApiSchema
            {
                Type = "String",
                Enum = { new OpenApiString("pt-BR"), new OpenApiString("en-US") }
            }
        });
    }
}

internal static class DocumentationExtension
{
    internal static WebApplicationBuilder ConfigureDocumentation(this WebApplicationBuilder builder)
    {
        OpenApiSecurityScheme openApiSecurityScheme = new()
        {
            In = ParameterLocation.Header,
            Description = "Please enter a valid token.",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        };

        OpenApiReference openApiReference = new()
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        };

        OpenApiSecurityRequirement openApiSecurityRequirement = new()
        {
            {
                new OpenApiSecurityScheme { Reference = openApiReference },
                Array.Empty<string>()
            }
        };

        builder.Services
            .AddEndpointsApiExplorer()
            .AddSwaggerExamplesFromAssemblyOf<BaseEndpoint>()
            .AddSwaggerGen(option =>
            {
                option.AddSecurityDefinition("Bearer", openApiSecurityScheme);
                option.AddSecurityRequirement(openApiSecurityRequirement);
                option.EnableAnnotations();
                option.ExampleFilters();
                option.OperationFilter<AddHeaderParameter>();
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Portfolio API", Version = "v1" });
            });

        return builder;
    }
}

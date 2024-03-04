using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Portfolio.Api.Configurations;

public sealed class AddHeaderParameter : IOperationFilter
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
                Enum = { new OpenApiString("en-US"), new OpenApiString("pt-BR") }
            }
        });
    }
}

public static class DocumentationConfiguration
{
    public static WebApplicationBuilder ConfigureDocumentation(this WebApplicationBuilder builder)
    {
        var openApiSecurityScheme = new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter a valid token.",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        };

        var openApiReference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        };

        var openApiSecurityRequirement = new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme { Reference = openApiReference },
                Array.Empty<string>()
            }
        };

        builder.Services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen(option =>
            {
                option.AddSecurityDefinition("Bearer", openApiSecurityScheme);
                option.AddSecurityRequirement(openApiSecurityRequirement);
                option.OperationFilter<AddHeaderParameter>();
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Portfolio API", Version = "v1" });
            });

        return builder;
    }
}

using Microsoft.OpenApi.Models;

namespace Portfolio.Api.Configurations;

public static class DocumentationConfiguration
{
    public static WebApplicationBuilder ConfigureDocumentation(this WebApplicationBuilder builder)
    {
        var openApiSecurityScheme = new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter a valid token",
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
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", openApiSecurityScheme);
                option.AddSecurityRequirement(openApiSecurityRequirement);
            });

        return builder;
    }
}

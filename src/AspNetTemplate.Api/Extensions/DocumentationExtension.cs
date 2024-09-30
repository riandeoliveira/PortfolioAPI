using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

using System.Reflection;

namespace AspNetTemplate.Api.Extensions;

internal static class DocumentationExtension
{
    internal static WebApplicationBuilder ConfigureDocumentation(this WebApplicationBuilder builder)
    {
        OpenApiInfo apiInfo = new()
        {
            Title = "AspNetTemplate API",
            Version = "v1"
        };

        builder.Services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen(options =>
            {
                options.EnableAnnotations();
                options.OperationFilter<AddHeaderParameter>();
                options.SwaggerDoc("v1", apiInfo);
                options.ExampleFilters();
            });

        Assembly? applicationAssembly = Assembly.Load("AspNetTemplate.Application");

        builder.Services.AddSwaggerExamplesFromAssemblies(applicationAssembly);

        return builder;
    }

    internal static WebApplication UseDocumentation(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        return app;
    }
}

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

using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

namespace AspNetTemplate.WebApi.Extensions;

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
        builder.Services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen(option =>
            {
                option.EnableAnnotations();
                option.OperationFilter<AddHeaderParameter>();
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "AspNetTemplate API", Version = "v1" });
                option.ExampleFilters();
            });

        Assembly? applicationAssembly = Assembly.Load("AspNetTemplate.Application");

        builder.Services.AddSwaggerExamplesFromAssemblies(applicationAssembly);

        return builder;
    }
}

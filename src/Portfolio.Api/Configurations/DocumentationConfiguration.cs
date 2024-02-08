namespace Portfolio.Api.Configurations;

public static class DocumentationConfiguration
{
    public static IServiceCollection ConfigureDocumentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer().AddSwaggerGen();

        return services;
    }
}

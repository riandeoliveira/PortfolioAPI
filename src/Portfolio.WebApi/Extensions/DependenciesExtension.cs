using Portfolio.Application.Extensions;
using Portfolio.Infrastructure.Extensions;

namespace Portfolio.WebApi.Extensions;

public static class DependenciesExtension
{
    public static WebApplicationBuilder ConfigureDependencies(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddAuthorDependencies()
            .AddInfraDependencies()
            .AddUserDependencies();

        return builder;
    }
}

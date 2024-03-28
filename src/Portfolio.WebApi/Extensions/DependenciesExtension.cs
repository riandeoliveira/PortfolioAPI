using Portfolio.Application.Extensions;
using Portfolio.Infrastructure.Extensions;

namespace Portfolio.WebApi.Extensions;

internal static class DependenciesExtension
{
    internal static WebApplicationBuilder ConfigureDependencies(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddAuthorDependencies()
            .AddInfraDependencies()
            .AddUserDependencies();

        return builder;
    }
}

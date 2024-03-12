using Portfolio.Authors.Extensions;
using Portfolio.Users.Extensions;
using Portfolio.Utils.Extensions;

namespace Portfolio.Api.Configurations;

public static class DependenciesConfiguration
{
    public static WebApplicationBuilder ConfigureDependencies(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddAuthorDependencies()
            .AddUserDependencies()
            .AddUtilsDependencies();

        return builder;
    }
}

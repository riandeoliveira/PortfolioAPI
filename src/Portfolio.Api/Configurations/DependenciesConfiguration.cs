using Portfolio.Authors.Interfaces;
using Portfolio.Authors.Repositories;
using Portfolio.Users.Interfaces;
using Portfolio.Users.Repositories;
using Portfolio.Utils.Interfaces;
using Portfolio.Utils.Services;

namespace Portfolio.Api.Configurations;

public static class DependenciesConfiguration
{
    public static IServiceCollection ConfigureDependencies(this IServiceCollection services)
    {
        services
            .AddScoped<IAuthorRepository, AuthorRepository>()
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<IUserRepository, UserRepository>()
            .AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        return services;
    }
}

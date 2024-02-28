using Portfolio.Authors.Interfaces;
using Portfolio.Authors.Repositories;
using Portfolio.Users.Interfaces;
using Portfolio.Users.Repositories;
using Portfolio.Utils.Interfaces;
using Portfolio.Utils.Services;

namespace Portfolio.Api.Configurations;

public static class DependenciesConfiguration
{
    public static WebApplicationBuilder ConfigureDependencies(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddScoped<IAuthorRepository, AuthorRepository>()
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<ILocalizationService, LocalizationService>()
            .AddScoped<IUserRepository, UserRepository>()
            .AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        return builder;
    }
}

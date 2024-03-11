using Portfolio.Authors.Features.Create;
using Portfolio.Authors.Features.Remove;
using Portfolio.Authors.Features.Update;
using Portfolio.Authors.Interfaces;
using Portfolio.Authors.Repositories;
using Portfolio.Users.Features.SignIn;
using Portfolio.Users.Features.SignUp;
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
            // Repositories
            .AddScoped<IAuthorRepository, AuthorRepository>()
            .AddScoped<IUserRepository, UserRepository>()

            // Services
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<ILocalizationService, LocalizationService>()

            // Validators
            .AddScoped<CreateAuthorValidator>()
            .AddScoped<RemoveAuthorValidator>()
            .AddScoped<SignInUserValidator>()
            .AddScoped<SignUpUserValidator>()
            .AddScoped<UpdateAuthorValidator>()

            // Others
            .AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        return builder;
    }
}

using MediatR;

using Portfolio.Domain.Interfaces;
using Portfolio.Infrastructure.Behaviors;
using Portfolio.Infrastructure.Repositories;
using Portfolio.Infrastructure.SeedWork;
using Portfolio.Infrastructure.Services;

namespace Portfolio.WebApi.Extensions;

internal static class DependenciesExtension
{
    internal static WebApplicationBuilder ConfigureDependencies(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddScoped<IAuthorRepository, AuthorRepository>()
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<ILocalizationService, LocalizationService>()
            .AddScoped<IMailService, MailService>()
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<IUserRepository, UserRepository>()

            .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()

            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ConversionBehavior<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return builder;
    }
}

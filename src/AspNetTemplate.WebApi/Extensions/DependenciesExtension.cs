using MediatR;

using AspNetTemplate.Domain.Interfaces;
using AspNetTemplate.Infrastructure.Behaviors;
using AspNetTemplate.Infrastructure.Repositories;
using AspNetTemplate.Infrastructure.SeedWork;
using AspNetTemplate.Infrastructure.Services;

namespace AspNetTemplate.WebApi.Extensions;

internal static class DependenciesExtension
{
    internal static WebApplicationBuilder ConfigureDependencies(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<IMailService, MailService>()
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<IUserRepository, UserRepository>()

            .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()

            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ConversionBehavior<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return builder;
    }
}

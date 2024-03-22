using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

using Portfolio.Domain.Interfaces;
using Portfolio.Infrastructure.Services;

namespace Portfolio.Infrastructure.Extensions;

public static class InfraExtension
{
    public static IServiceCollection AddInfraDependencies(this IServiceCollection services)
    {
        return services
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<ILocalizationService, LocalizationService>()
            .AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    }
}

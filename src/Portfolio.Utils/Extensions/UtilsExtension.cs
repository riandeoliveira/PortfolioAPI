using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

using Portfolio.Utils.Interfaces;
using Portfolio.Utils.Services;

namespace Portfolio.Utils.Extensions;

public static class UtilsExtension
{
    public static IServiceCollection AddUtilsDependencies(this IServiceCollection services)
    {
        return services
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<ILocalizationService, LocalizationService>()
            .AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    }
}

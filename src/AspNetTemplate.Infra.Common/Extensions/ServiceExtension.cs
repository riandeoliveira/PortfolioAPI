using AspNetTemplate.Infra.Common.Services;
using AspNetTemplate.Infra.Data.Interfaces;

using Microsoft.Extensions.DependencyInjection;

namespace AspNetTemplate.Infra.Common.Extensions;

public static class ServiceExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<IMailService, MailService>();

        return services;
    }
}

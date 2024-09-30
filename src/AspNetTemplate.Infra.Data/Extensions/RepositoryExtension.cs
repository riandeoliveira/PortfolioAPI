using AspNetTemplate.Infra.Data.Interfaces;
using AspNetTemplate.Infra.Data.Repositories;
using AspNetTemplate.Infra.Data.SeedWork;

using Microsoft.Extensions.DependencyInjection;

namespace AspNetTemplate.Infra.Data.Extensions;

public static class RepositoryExtension
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services
            .AddScoped<IPersonalRefreshTokenRepository, PersonalRefreshTokenRepository>()
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}

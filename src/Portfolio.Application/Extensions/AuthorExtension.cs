using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

using Portfolio.Domain.Interfaces;
using Portfolio.Infrastructure.Repositories;

namespace Portfolio.Application.Extensions;

public static class AuthorExtension
{
    public static IServiceCollection AddAuthorDependencies(this IServiceCollection services)
    {
        return services
            .AddMediatR(configuration =>
                configuration.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly())
            )
            .AddScoped<IAuthorRepository, AuthorRepository>();
    }
}

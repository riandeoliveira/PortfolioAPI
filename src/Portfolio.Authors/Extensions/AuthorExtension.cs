using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

using Portfolio.Authors.Features.Create;
using Portfolio.Authors.Features.FindMany;
using Portfolio.Authors.Features.FindOne;
using Portfolio.Authors.Features.Remove;
using Portfolio.Authors.Features.Update;
using Portfolio.Authors.Interfaces;
using Portfolio.Authors.Repositories;

namespace Portfolio.Authors.Extensions;

public static class AuthorExtension
{
    public static IServiceCollection AddAuthorDependencies(this IServiceCollection services)
    {
        return services
            .AddMediatR(configuration =>
                configuration.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly())
            )

            .AddScoped<CreateAuthorValidator>()
            .AddScoped<FindManyAuthorsValidator>()
            .AddScoped<FindOneAuthorValidator>()
            .AddScoped<IAuthorRepository, AuthorRepository>()
            .AddScoped<RemoveAuthorValidator>()
            .AddScoped<UpdateAuthorValidator>();
    }
}

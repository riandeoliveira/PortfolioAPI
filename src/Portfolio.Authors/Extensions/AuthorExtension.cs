using Microsoft.Extensions.DependencyInjection;

using Portfolio.Authors.Features.Create;
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
            .AddScoped<CreateAuthorValidator>()
            .AddScoped<IAuthorRepository, AuthorRepository>()
            .AddScoped<RemoveAuthorValidator>()
            .AddScoped<UpdateAuthorValidator>();
    }
}

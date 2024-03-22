using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

using Portfolio.Application.UseCases.CreateAuthor;
using Portfolio.Application.UseCases.FindManyAuthors;
using Portfolio.Application.UseCases.FindOneAuthor;
using Portfolio.Application.UseCases.RemoveAuthor;
using Portfolio.Application.UseCases.UpdateAuthor;
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

            .AddScoped<CreateAuthorValidator>()
            .AddScoped<FindManyAuthorsValidator>()
            .AddScoped<FindOneAuthorValidator>()
            .AddScoped<IAuthorRepository, AuthorRepository>()
            .AddScoped<RemoveAuthorValidator>()
            .AddScoped<UpdateAuthorValidator>();
    }
}

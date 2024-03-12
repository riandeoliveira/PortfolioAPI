using System.Reflection;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

using Portfolio.Users.Features.SignIn;
using Portfolio.Users.Features.SignUp;
using Portfolio.Users.Interfaces;
using Portfolio.Users.Repositories;

namespace Portfolio.Users.Extensions;

public static class UserExtension
{
    public static IServiceCollection AddUserDependencies(this IServiceCollection services)
    {
        return services
            .AddMediatR(configuration =>
                configuration.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly())
            )

            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<SignInUserValidator>()
            .AddScoped<SignUpUserValidator>();
    }
}
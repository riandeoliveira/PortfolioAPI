using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

using Portfolio.Application.UseCases.FindCurrentUser;
using Portfolio.Application.UseCases.ForgotUserPassword;
using Portfolio.Application.UseCases.RemoveUser;
using Portfolio.Application.UseCases.ResetUserPassword;
using Portfolio.Application.UseCases.SignInUser;
using Portfolio.Application.UseCases.SignUpUser;
using Portfolio.Application.UseCases.UpdateUser;
using Portfolio.Domain.Interfaces;
using Portfolio.Infrastructure.Repositories;

namespace Portfolio.Application.Extensions;

public static class UserExtension
{
    public static IServiceCollection AddUserDependencies(this IServiceCollection services)
    {
        return services
            .AddMediatR(configuration =>
                configuration.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly())
            )
            .AddScoped<IUserRepository, UserRepository>();
    }
}

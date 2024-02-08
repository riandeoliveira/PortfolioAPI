using System.Reflection;
using System.Text.Json;

using Portfolio.Users.Handlers;
using Portfolio.Authors.Handlers;

namespace Portfolio.Api.Configurations;

public static class ControllerConfiguration
{
    public static IServiceCollection ConfigureControllers(this IServiceCollection services)
    {
        services
            .AddMediatR(cfg => 
            {
                cfg.RegisterServicesFromAssemblies(typeof(CreateAuthorHandler).GetTypeInfo().Assembly);
                cfg.RegisterServicesFromAssemblies(typeof(DeleteAuthorHandler).GetTypeInfo().Assembly);
                cfg.RegisterServicesFromAssemblies(typeof(LoginUserHandler).GetTypeInfo().Assembly);
                cfg.RegisterServicesFromAssemblies(typeof(SignInUserHandler).GetTypeInfo().Assembly);
            })
            .AddControllers()
            .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower);

        return services;
    }
}

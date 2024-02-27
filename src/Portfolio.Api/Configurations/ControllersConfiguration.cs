using Portfolio.Authors.Handlers;
using Portfolio.Users.Handlers;

using System.Reflection;
using System.Text.Json;

namespace Portfolio.Api.Configurations;

public static class ControllerConfiguration
{
    public static IServiceCollection ConfigureControllers(this IServiceCollection services)
    {
        services
            .AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssemblies(typeof(CreateAuthorHandler).GetTypeInfo().Assembly);
                configuration.RegisterServicesFromAssemblies(typeof(RemoveAuthorHandler).GetTypeInfo().Assembly);
                configuration.RegisterServicesFromAssemblies(typeof(SignInUserHandler).GetTypeInfo().Assembly);
                configuration.RegisterServicesFromAssemblies(typeof(SignUpUserHandler).GetTypeInfo().Assembly);
            })
            .AddControllers()
            .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower);

        return services;
    }
}

using System.Reflection;
using System.Text.Json;

using Portfolio.Authors.Features.Create;
using Portfolio.Authors.Features.Remove;
using Portfolio.Users.Features.SignIn;
using Portfolio.Users.Features.SignUp;

using Serilog;

namespace Portfolio.Api.Configurations;

public static class ControllerConfiguration
{
    public static WebApplicationBuilder ConfigureControllers(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssemblies(typeof(CreateAuthorHandler).GetTypeInfo().Assembly);
                configuration.RegisterServicesFromAssemblies(typeof(RemoveAuthorHandler).GetTypeInfo().Assembly);
                configuration.RegisterServicesFromAssemblies(typeof(SignInUserHandler).GetTypeInfo().Assembly);
                configuration.RegisterServicesFromAssemblies(typeof(SignUpUserHandler).GetTypeInfo().Assembly);
            })
            .AddProblemDetails()
            .AddControllers()
            .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower);

        builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

        return builder;
    }
}

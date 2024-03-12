using SharpDotEnv;

namespace Portfolio.Api.Configurations;

public static class EnvironmentConfiguration
{
    public static WebApplicationBuilder ConfigureEnvironment(this WebApplicationBuilder builder)
    {
        DotEnv.Config();

        return builder;
    }
}

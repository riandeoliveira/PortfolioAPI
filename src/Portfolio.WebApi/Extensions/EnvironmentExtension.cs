using SharpDotEnv;

namespace Portfolio.WebApi.Extensions;

public static class EnvironmentExtension
{
    public static WebApplicationBuilder ConfigureEnvironment(this WebApplicationBuilder builder)
    {
        DotEnv.Config();

        return builder;
    }
}

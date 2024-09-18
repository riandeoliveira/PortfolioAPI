using SharpDotEnv;

namespace AspNetTemplate.WebApi.Extensions;

internal static class EnvironmentExtension
{
    internal static WebApplicationBuilder ConfigureEnvironment(this WebApplicationBuilder builder)
    {
        DotEnv.Config();

        return builder;
    }
}

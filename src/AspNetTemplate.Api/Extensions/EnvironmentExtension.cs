using SharpDotEnv;

namespace AspNetTemplate.Api.Extensions;

internal static class EnvironmentExtension
{
    internal static WebApplicationBuilder ConfigureEnvironment(this WebApplicationBuilder builder)
    {
        DotEnv.Config();

        return builder;
    }
}

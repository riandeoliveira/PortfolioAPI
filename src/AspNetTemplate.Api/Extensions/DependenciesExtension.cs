using AspNetTemplate.Application.Extensions;
using AspNetTemplate.Infra.Common.Extensions;
using AspNetTemplate.Infra.Data.Extensions;

namespace AspNetTemplate.Api.Extensions;

internal static class DependenciesExtension
{
    internal static WebApplicationBuilder ConfigureDependencies(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddPipelineBehaviors()
            .AddRepositories()
            .AddServices()
            .AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        return builder;
    }
}

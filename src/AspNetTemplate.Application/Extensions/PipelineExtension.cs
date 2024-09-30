using AspNetTemplate.Application.Behaviors;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

namespace AspNetTemplate.Application.Extensions;

public static class PipelineExtension
{
    public static IServiceCollection AddPipelineBehaviors(this IServiceCollection services)
    {
        services
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ConversionBehavior<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}

using System.Reflection;

using MediatR;

namespace AspNetTemplate.Infrastructure.Behaviors;

public sealed class ConversionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken = default
    )
    {
        TrimAllStringProperties(request);

        return await next();
    }

    private static void TrimAllStringProperties(TRequest request)
    {
        IEnumerable<PropertyInfo> stringProperties = request
            .GetType()
            .GetProperties()
            .Where(property => property.PropertyType == typeof(string));

        foreach (PropertyInfo property in stringProperties)
        {
            string? value = (string?) property.GetValue(request);

            if (value is not null) property.SetValue(request, value.Trim());
        }
    }
}

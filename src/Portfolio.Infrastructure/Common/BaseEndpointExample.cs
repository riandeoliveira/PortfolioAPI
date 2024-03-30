using Bogus;

using Swashbuckle.AspNetCore.Filters;

namespace Portfolio.Infrastructure.Common;

public abstract class BaseEndpointExample<TRequest> : IExamplesProvider<TRequest?>
{
    protected Faker _faker = new();

    public virtual TRequest? GetExamples()
    {
        return default;
    }
}

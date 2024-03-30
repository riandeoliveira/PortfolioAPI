using Portfolio.Application.UseCases.FindOneAuthor;
using Portfolio.Infrastructure.Common;

namespace Portfolio.Application.UseCases.FindOneAuthor;

public sealed class FindOneAuthorExample : BaseEndpointExample<FindOneAuthorRequest>
{
    public override FindOneAuthorRequest GetExamples()
    {
        return new FindOneAuthorRequest(_faker.Random.Guid());
    }
}

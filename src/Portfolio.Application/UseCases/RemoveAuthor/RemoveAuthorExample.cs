using Portfolio.Infrastructure.Common;

namespace Portfolio.Application.UseCases.RemoveAuthor;

public sealed class RemoveAuthorExample : BaseEndpointExample<RemoveAuthorRequest>
{
    public override RemoveAuthorRequest GetExamples()
    {
        return new RemoveAuthorRequest(_faker.Random.Guid());
    }
}

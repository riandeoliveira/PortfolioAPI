using Portfolio.Infrastructure.Common;

namespace Portfolio.Application.UseCases.FindManyAuthors;

public sealed class FindManyAuthorsExample : BaseEndpointExample<FindManyAuthorsRequest>
{
    public override FindManyAuthorsRequest GetExamples()
    {
        return new FindManyAuthorsRequest();
    }
}

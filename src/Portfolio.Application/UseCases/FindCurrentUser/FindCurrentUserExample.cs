using Portfolio.Infrastructure.Common;

namespace Portfolio.Application.UseCases.FindCurrentUser;

public sealed class FindCurrentUserExample : BaseEndpointExample<FindCurrentUserRequest>
{
    public override FindCurrentUserRequest GetExamples()
    {
        return new FindCurrentUserRequest();
    }
}

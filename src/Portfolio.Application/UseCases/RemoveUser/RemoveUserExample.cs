using Portfolio.Infrastructure.Common;

namespace Portfolio.Application.UseCases.RemoveUser;

public sealed class RemoveUserExample : BaseEndpointExample<RemoveUserRequest>
{
    public override RemoveUserRequest GetExamples()
    {
        return new RemoveUserRequest();
    }
}

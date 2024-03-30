using Portfolio.Infrastructure.Common;

namespace Portfolio.Application.UseCases.UpdateUser;

public sealed class UpdateUserExample : BaseEndpointExample<UpdateUserRequest>
{
    public override UpdateUserRequest GetExamples()
    {
        return new UpdateUserRequest(
            _faker.Random.Guid(),
            _faker.Internet.Email(),
            _faker.Internet.Password()
        );
    }
}

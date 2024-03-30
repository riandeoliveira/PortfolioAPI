using Portfolio.Infrastructure.Common;

namespace Portfolio.Application.UseCases.SignUpUser;

public sealed class SignUpUserExample : BaseEndpointExample<SignUpUserRequest>
{
    public override SignUpUserRequest GetExamples()
    {
        return new SignUpUserRequest(
            _faker.Internet.Email(),
            _faker.Internet.Password()
        );
    }
}

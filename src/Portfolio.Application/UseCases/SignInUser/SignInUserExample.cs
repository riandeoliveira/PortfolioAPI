using Portfolio.Infrastructure.Common;

namespace Portfolio.Application.UseCases.SignInUser;

public sealed class SignInUserExample : BaseEndpointExample<SignInUserRequest>
{
    public override SignInUserRequest GetExamples()
    {
        return new SignInUserRequest(
            _faker.Internet.Email(),
            _faker.Internet.Password()
        );
    }
}

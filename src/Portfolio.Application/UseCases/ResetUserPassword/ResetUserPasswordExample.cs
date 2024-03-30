using Portfolio.Infrastructure.Common;

namespace Portfolio.Application.UseCases.ResetUserPassword;

public sealed class ResetUserPasswordExample : BaseEndpointExample<ResetUserPasswordRequest>
{
    public override ResetUserPasswordRequest GetExamples()
    {
        string password = _faker.Internet.Password();

        return new ResetUserPasswordRequest(password, password);
    }
}

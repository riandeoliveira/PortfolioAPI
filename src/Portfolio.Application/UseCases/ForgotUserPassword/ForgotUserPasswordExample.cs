using Portfolio.Infrastructure.Common;

namespace Portfolio.Application.UseCases.ForgotUserPassword;

public sealed class ForgotUserPasswordExample : BaseEndpointExample<ForgotUserPasswordRequest>
{
    public override ForgotUserPasswordRequest GetExamples()
    {
        return new ForgotUserPasswordRequest(_faker.Internet.Email());
    }
}

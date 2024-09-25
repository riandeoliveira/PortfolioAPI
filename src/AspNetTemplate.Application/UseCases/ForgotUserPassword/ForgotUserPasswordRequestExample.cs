using Bogus;

using Swashbuckle.AspNetCore.Filters;

namespace AspNetTemplate.Application.UseCases.ForgotUserPassword;

public class ForgotUserPasswordRequestExample : IExamplesProvider<ForgotUserPasswordRequest>
{
    public ForgotUserPasswordRequest GetExamples()
    {
        Faker faker = new();

        string email = faker.Internet.Email();

        return new ForgotUserPasswordRequest(email);
    }
}

using AspNetTemplate.Infrastructure.Extensions;

using Bogus;

using Swashbuckle.AspNetCore.Filters;

namespace AspNetTemplate.Application.UseCases.ResetUserPassword;

public class ResetUserPasswordRequestExample : IExamplesProvider<ResetUserPasswordRequest>
{
    public ResetUserPasswordRequest GetExamples()
    {
        Faker faker = new();

        string password = faker.Internet.StrongPassword();

        return new ResetUserPasswordRequest(password, password);
    }
}

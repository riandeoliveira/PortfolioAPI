using AspNetTemplate.Infra.Common.Extensions;

using Bogus;

using Swashbuckle.AspNetCore.Filters;

namespace AspNetTemplate.Application.UseCases.SignInUser;

public class SignInUserRequestExample : IExamplesProvider<SignInUserRequest>
{
    public SignInUserRequest GetExamples()
    {
        Faker faker = new();

        string email = faker.Internet.Email();
        string password = faker.Internet.StrongPassword();

        return new SignInUserRequest(email, password);
    }
}

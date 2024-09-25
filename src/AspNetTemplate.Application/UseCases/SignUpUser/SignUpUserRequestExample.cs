using AspNetTemplate.Infrastructure.Extensions;

using Bogus;

using Swashbuckle.AspNetCore.Filters;

namespace AspNetTemplate.Application.UseCases.SignUpUser;

public class SignUpUserRequestExample : IExamplesProvider<SignUpUserRequest>
{
    public SignUpUserRequest GetExamples()
    {
        Faker faker = new();

        string email = faker.Internet.Email();
        string password = faker.Internet.StrongPassword();

        return new SignUpUserRequest(email, password);
    }
}

using AspNetTemplate.Infra.Common.Extensions;

using Bogus;

using Swashbuckle.AspNetCore.Filters;

namespace AspNetTemplate.Application.UseCases.UpdateUser;

public class UpdateUserRequestExample : IExamplesProvider<UpdateUserRequest>
{
    public UpdateUserRequest GetExamples()
    {
        Faker faker = new();

        string email = faker.Internet.Email();
        string password = faker.Internet.StrongPassword();

        return new UpdateUserRequest(email, password);
    }
}

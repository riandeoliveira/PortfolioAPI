using Portfolio.Infrastructure.Common;

namespace Portfolio.Application.UseCases.CreateAuthor;

public sealed class CreateAuthorExample : BaseEndpointExample<CreateAuthorRequest>
{
    public override CreateAuthorRequest GetExamples()
    {
        return new CreateAuthorRequest(
            _faker.Name.FirstName(),
            _faker.Name.FullName(),
            _faker.Name.JobTitle(),
            _faker.Lorem.Sentence(),
            _faker.Internet.Url(),
            _faker.Internet.UserName()
        );
    }
}

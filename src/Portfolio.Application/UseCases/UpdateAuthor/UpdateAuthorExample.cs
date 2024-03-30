using Portfolio.Infrastructure.Common;

namespace Portfolio.Application.UseCases.UpdateAuthor;

public sealed class UpdateAuthorExample : BaseEndpointExample<UpdateAuthorRequest>
{
    public override UpdateAuthorRequest GetExamples()
    {
        return new UpdateAuthorRequest(
            _faker.Random.Guid(),
            _faker.Name.FirstName(),
            _faker.Name.FullName(),
            _faker.Name.JobTitle(),
            _faker.Lorem.Sentence(),
            _faker.Internet.Url(),
            _faker.Internet.UserName()
        );
    }
}

using Portfolio.Authors.Interfaces;
using Portfolio.Domain.Entities;
using Portfolio.Utils.Extensions;
using Portfolio.Utils.Messaging;

namespace Portfolio.Authors.Features.FindOne;

public sealed class FindOneAuthorHandler(
    IAuthorRepository authorRepository,
    FindOneAuthorValidator validator
) : IRequestHandler<FindOneAuthorRequest, FindOneAuthorResponse>
{
    public async Task<FindOneAuthorResponse> Handle(FindOneAuthorRequest request, CancellationToken cancellationToken = default)
    {
        await validator.ValidateOrThrowAsync(request, cancellationToken);

        Author author = await authorRepository.FindOneOrThrowAsync(request.Id, cancellationToken);

        return new FindOneAuthorResponse(author);
    }
}

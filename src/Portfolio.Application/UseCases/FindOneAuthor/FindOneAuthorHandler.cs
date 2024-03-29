using Mapster;

using Portfolio.Domain.Dtos;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.UseCases.FindOneAuthor;

public sealed class FindOneAuthorHandler(
    IAuthorRepository authorRepository
) : IRequestHandler<FindOneAuthorRequest, FindOneAuthorResponse>
{
    public async Task<FindOneAuthorResponse> Handle(FindOneAuthorRequest request, CancellationToken cancellationToken = default)
    {
        Author author = await authorRepository.FindOneOrThrowAsync(request.Id, cancellationToken);

        return new FindOneAuthorResponse(author.Adapt<AuthorDto>());
    }
}

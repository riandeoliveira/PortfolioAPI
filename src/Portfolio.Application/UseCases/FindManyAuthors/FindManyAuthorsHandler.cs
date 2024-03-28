using Mapster;

using Portfolio.Domain.Dtos;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces;
using Portfolio.Infrastructure.Extensions;

namespace Portfolio.Application.UseCases.FindManyAuthors;

public sealed class FindManyAuthorsHandler(
    FindManyAuthorsValidator validator,
    IAuthorRepository authorRepository,
    IAuthService authService
) : IRequestHandler<FindManyAuthorsRequest, FindManyAuthorsResponse>
{
    public async Task<FindManyAuthorsResponse> Handle(FindManyAuthorsRequest request, CancellationToken cancellationToken = default)
    {
        await validator.ValidateOrThrowAsync(request, cancellationToken);

        IEnumerable<Author> authors = await authorRepository.FindManyAsync(
            author => author.UserId == authService.GetLoggedInUserId(),
            cancellationToken
        );

        return new FindManyAuthorsResponse(authors.Adapt<IEnumerable<AuthorDto>>());
    }
}

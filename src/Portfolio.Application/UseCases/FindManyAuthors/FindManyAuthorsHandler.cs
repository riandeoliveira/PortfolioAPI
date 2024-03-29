using Mapster;

using Portfolio.Domain.Dtos;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.UseCases.FindManyAuthors;

public sealed class FindManyAuthorsHandler(
    IAuthorRepository authorRepository,
    IAuthService authService
) : IRequestHandler<FindManyAuthorsRequest, FindManyAuthorsResponse>
{
    public async Task<FindManyAuthorsResponse> Handle(FindManyAuthorsRequest request, CancellationToken cancellationToken = default)
    {
        IEnumerable<Author> authors = await authorRepository.FindManyAsync(
            author => author.UserId == authService.GetLoggedInUserId(),
            cancellationToken
        );

        return new FindManyAuthorsResponse(authors.Adapt<IEnumerable<AuthorDto>>());
    }
}

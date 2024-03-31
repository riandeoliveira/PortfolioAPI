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
        PaginationDto<Author> response = await authorRepository.PaginateAsync(
            author => author.UserId == authService.GetLoggedInUserId(),
            request.PageNumber,
            request.PageSize,
            cancellationToken
        );

        return new FindManyAuthorsResponse(
            response.PageNumber,
            response.PageSize,
            response.TotalPages,
            response.Entities.Adapt<IEnumerable<AuthorDto>>()
        );
    }
}

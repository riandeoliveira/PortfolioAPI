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
        UserDto userDto = await authService.GetCurrentUserAsync(cancellationToken);

        PaginationDto<Author> paginationDto = await authorRepository.PaginateAsync(
            author => author.UserId == userDto.Id,
            request.PageNumber,
            request.PageSize,
            cancellationToken
        );

        return new FindManyAuthorsResponse(
            paginationDto.PageNumber,
            paginationDto.PageSize,
            paginationDto.TotalPages,
            paginationDto.Entities.Adapt<IEnumerable<AuthorDto>>()
        );
    }
}

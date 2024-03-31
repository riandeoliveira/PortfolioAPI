using Portfolio.Domain.Dtos;

namespace Portfolio.Application.UseCases.FindManyAuthors;

public sealed record FindManyAuthorsResponse(
    int PageNumber,
    int PageSize,
    int TotalPages,
    IEnumerable<AuthorDto> Entities
);

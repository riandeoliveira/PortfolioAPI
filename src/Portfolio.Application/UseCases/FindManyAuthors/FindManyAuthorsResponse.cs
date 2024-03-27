using Portfolio.Domain.Dtos;

namespace Portfolio.Application.UseCases.FindManyAuthors;

public sealed record FindManyAuthorsResponse(IEnumerable<AuthorDto> Authors);

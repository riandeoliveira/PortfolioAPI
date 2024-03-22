using Portfolio.Domain.Entities;

namespace Portfolio.Application.UseCases.FindManyAuthors;

public sealed record FindManyAuthorsResponse(IEnumerable<Author> Authors);

using Portfolio.Domain.Entities;

namespace Portfolio.Authors.Features.FindMany;

public sealed record FindManyAuthorsResponse(IEnumerable<Author> Authors);

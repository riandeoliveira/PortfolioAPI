using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.UseCases.FindManyAuthors;

public sealed record FindManyAuthorsRequest : IRequest<FindManyAuthorsResponse>;

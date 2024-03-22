using MediatR;

namespace Portfolio.Application.UseCases.FindManyAuthors;

public sealed record FindManyAuthorsRequest : IRequest<FindManyAuthorsResponse>;

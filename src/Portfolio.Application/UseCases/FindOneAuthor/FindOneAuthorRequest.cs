using MediatR;

namespace Portfolio.Application.UseCases.FindOneAuthor;

public sealed record FindOneAuthorRequest(Guid Id) : IRequest<FindOneAuthorResponse>;

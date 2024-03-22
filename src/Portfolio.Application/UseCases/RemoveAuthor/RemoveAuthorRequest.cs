using MediatR;

namespace Portfolio.Application.UseCases.RemoveAuthor;

public sealed record RemoveAuthorRequest(Guid Id) : IRequest<RemoveAuthorResponse>;

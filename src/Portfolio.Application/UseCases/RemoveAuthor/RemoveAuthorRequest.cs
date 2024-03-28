using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.UseCases.RemoveAuthor;

public sealed record RemoveAuthorRequest(Guid Id) : IRequest<RemoveAuthorResponse>;

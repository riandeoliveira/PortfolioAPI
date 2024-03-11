using Portfolio.Utils.Messaging;

namespace Portfolio.Authors.Features.Remove;

public sealed record RemoveAuthorRequest(Guid Id) : IRequest<RemoveAuthorResponse>;

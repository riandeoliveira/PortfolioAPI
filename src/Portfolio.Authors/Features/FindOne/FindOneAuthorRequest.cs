using Portfolio.Utils.Messaging;

namespace Portfolio.Authors.Features.FindOne;

public sealed record FindOneAuthorRequest(Guid Id) : IRequest<FindOneAuthorResponse>;

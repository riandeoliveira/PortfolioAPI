using Portfolio.Utils.Messaging;

namespace Portfolio.Authors.Requests;

public sealed record DeleteAuthorRequest(Guid Id) : IRequest;

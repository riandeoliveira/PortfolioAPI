using Portfolio.Utils.Messaging;

namespace Portfolio.Authors.Requests;

public sealed record RemoveAuthorRequest(Guid Id) : IRequest;

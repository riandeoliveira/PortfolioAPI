namespace Portfolio.Utils.Messaging;

public interface IRequest<TResponse> : MediatR.IRequest<TResponse>
{
}

public interface IRequest : MediatR.IRequest
{
}

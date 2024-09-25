namespace AspNetTemplate.Domain.Interfaces;

public interface IRequestHandler<TRequest> : MediatR.IRequestHandler<TRequest> where TRequest : MediatR.IRequest
{
}

public interface IRequestHandler<TRequest, TResponse> : MediatR.IRequestHandler<TRequest, TResponse> where TRequest : MediatR.IRequest<TResponse>
{
}

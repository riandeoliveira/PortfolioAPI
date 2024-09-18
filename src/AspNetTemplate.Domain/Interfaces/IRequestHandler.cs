namespace AspNetTemplate.Domain.Interfaces;

public interface IRequestHandler<TRequest, TResponse> : MediatR.IRequestHandler<TRequest, TResponse> where TRequest : MediatR.IRequest<TResponse>
{
}

namespace AspNetTemplate.Domain.Interfaces;

public interface IRequest : MediatR.IRequest
{
}

public interface IRequest<TResponse> : MediatR.IRequest<TResponse>
{
}

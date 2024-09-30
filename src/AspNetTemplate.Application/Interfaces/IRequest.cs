namespace AspNetTemplate.Application.Interfaces;

public interface IRequest : MediatR.IRequest
{
}

public interface IRequest<TResponse> : MediatR.IRequest<TResponse>
{
}

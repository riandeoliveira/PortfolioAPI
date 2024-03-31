using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.UseCases.FindManyAuthors;

public sealed record FindManyAuthorsRequest(int PageNumber, int PageSize) : IRequest<FindManyAuthorsResponse>;

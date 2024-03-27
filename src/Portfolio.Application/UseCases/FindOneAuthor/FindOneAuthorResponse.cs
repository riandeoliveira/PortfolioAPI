using Portfolio.Domain.Dtos;

namespace Portfolio.Application.UseCases.FindOneAuthor;

public sealed record FindOneAuthorResponse(AuthorDto Author);

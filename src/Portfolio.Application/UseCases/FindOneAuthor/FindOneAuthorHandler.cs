using Mapster;

using MediatR;

using Portfolio.Domain.Dtos;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces;
using Portfolio.Infrastructure.Extensions;

namespace Portfolio.Application.UseCases.FindOneAuthor;

public sealed class FindOneAuthorHandler(
    IAuthorRepository authorRepository,
    FindOneAuthorValidator validator
) : IRequestHandler<FindOneAuthorRequest, FindOneAuthorResponse>
{
    public async Task<FindOneAuthorResponse> Handle(FindOneAuthorRequest request, CancellationToken cancellationToken = default)
    {
        await validator.ValidateOrThrowAsync(request, cancellationToken);

        Author author = await authorRepository.FindOneOrThrowAsync(request.Id, cancellationToken);

        return new FindOneAuthorResponse(author.Adapt<AuthorDto>());
    }
}

using MediatR;

using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces;
using Portfolio.Infrastructure.Extensions;

namespace Portfolio.Application.UseCases.UpdateAuthor;

public sealed class UpdateAuthorHandler(
    IAuthorRepository authorRepository,
    UpdateAuthorValidator validator
) : IRequestHandler<UpdateAuthorRequest, UpdateAuthorResponse>
{
    public async Task<UpdateAuthorResponse> Handle(UpdateAuthorRequest request, CancellationToken cancellationToken = default)
    {
        await validator.ValidateOrThrowAsync(request, cancellationToken);

        Author author = await authorRepository.FindOneOrThrowAsync(request.Id, cancellationToken);

        author.Name = request.Name;
        author.FullName = request.FullName;
        author.Position = request.Position;
        author.Description = request.Description;
        author.AvatarUrl = request.AvatarUrl;
        author.SpotifyAccountName = request.SpotifyAccountName;

        await authorRepository.UpdateAsync(author, cancellationToken);
        await authorRepository.SaveChangesAsync(cancellationToken);

        return new UpdateAuthorResponse();
    }
}

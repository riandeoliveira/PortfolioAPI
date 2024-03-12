using Portfolio.Authors.Interfaces;
using Portfolio.Domain.Entities;
using Portfolio.Utils.Extensions;
using Portfolio.Utils.Messaging;

namespace Portfolio.Authors.Features.Update;

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
        author.UpdatedAt = DateTime.Now;

        await authorRepository.UpdateAsync(author, cancellationToken);
        await authorRepository.SaveChangesAsync(cancellationToken);

        return new UpdateAuthorResponse();
    }
}

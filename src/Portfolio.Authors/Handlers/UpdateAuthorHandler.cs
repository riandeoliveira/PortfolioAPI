using Portfolio.Authors.Interfaces;
using Portfolio.Authors.Requests;
using Portfolio.Authors.Validators;
using Portfolio.Domain.Entities;
using Portfolio.Utils.Extensions;
using Portfolio.Utils.Messaging;

namespace Portfolio.Authors.Handlers;

public sealed class UpdateAuthorHandler
(
    IAuthorRepository authorRepository,
    UpdateAuthorValidator validator
) : IRequestHandler<UpdateAuthorRequest, Author>
{
    public async Task<Author> Handle(UpdateAuthorRequest request, CancellationToken cancellationToken = default)
    {
        await validator.ValidateRequestAsync(request, cancellationToken);

        Author author = await authorRepository.FindOrThrowAsync(request.Id, cancellationToken);

        author.Name = request.Name;
        author.FullName = request.FullName;
        author.Position = request.Position;
        author.Description = request.Description;
        author.AvatarUrl = request.AvatarUrl;
        author.SpotifyAccountName = request.SpotifyAccountName;

        await authorRepository.UpdateAsync(author, cancellationToken);
        await authorRepository.SaveChangesAsync(cancellationToken);

        return author;
    }
}

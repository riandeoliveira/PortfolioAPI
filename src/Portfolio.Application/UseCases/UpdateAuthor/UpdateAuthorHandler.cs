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

        author.Name = request.Name.Trim();
        author.FullName = request.FullName.Trim();
        author.Position = request.Position.Trim();
        author.Description = request.Description.Trim();
        author.AvatarUrl = request.AvatarUrl.Trim();
        author.SpotifyAccountName = request.SpotifyAccountName?.Trim();

        await authorRepository.UpdateAsync(author, cancellationToken);
        await authorRepository.SaveChangesAsync(cancellationToken);

        return new UpdateAuthorResponse();
    }
}

using Portfolio.Authors.Interfaces;
using Portfolio.Authors.Requests;
using Portfolio.Domain.Entities;
using Portfolio.Utils.Interfaces;
using Portfolio.Utils.Messaging;

namespace Portfolio.Authors.Handlers;

public sealed class CreateAuthorHandler(IAuthorRepository authorRepository, IAuthService authService) : IRequestHandler<CreateAuthorRequest, Author>
{
    private readonly IAuthorRepository _authorRepository = authorRepository;
    private readonly IAuthService _authService = authService;

    public async Task<Author> Handle(CreateAuthorRequest request, CancellationToken cancellationToken = default)
    {
        // TODO: FAZER VALIDAÇÕES DO FLUENT AQUI !!!

        Author author = new()
        {
            Name = request.Name,
            FullName = request.FullName,
            Position = request.Position,
            Description = request.Description,
            AvatarUrl = request.AvatarUrl,
            SpotifyAccountName = request.SpotifyAccountName,
            UserId = _authService.GetLoggedInUserId()
        };

        Author createdAuthor = await _authorRepository.CreateAsync(author, cancellationToken);

        await _authorRepository.SaveChangesAsync(cancellationToken);

        return createdAuthor;
    }
}

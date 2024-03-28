using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces;
using Portfolio.Infrastructure.Extensions;

namespace Portfolio.Application.UseCases.RemoveUser;

public sealed class RemoveUserHandler(
    IAuthorRepository authorRepository,
    IAuthService authService,
    IUserRepository userRepository,
    RemoveUserValidator validator
) : IRequestHandler<RemoveUserRequest, RemoveUserResponse>
{
    public async Task<RemoveUserResponse> Handle(RemoveUserRequest request, CancellationToken cancellationToken = default)
    {
        await validator.ValidateOrThrowAsync(request, cancellationToken);

        User user = await userRepository.FindOneOrThrowAsync(
            authService.GetLoggedInUserId(),
            cancellationToken
        );

        IEnumerable<Author> authors = await authorRepository.FindManyAsync(
            author => author.UserId == authService.GetLoggedInUserId(),
            cancellationToken
        );

        foreach (Author author in authors)
        {
            await authorRepository.RemoveSoftAsync(author, cancellationToken);
        }

        await userRepository.RemoveSoftAsync(user, cancellationToken);

        await authorRepository.SaveChangesAsync(cancellationToken);
        await userRepository.SaveChangesAsync(cancellationToken);

        return new RemoveUserResponse();
    }
}

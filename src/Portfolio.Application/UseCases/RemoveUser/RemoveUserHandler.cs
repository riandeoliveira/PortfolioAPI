using Portfolio.Domain.Dtos;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.UseCases.RemoveUser;

public sealed class RemoveUserHandler(
    IAuthorRepository authorRepository,
    IAuthService authService,
    IUnitOfWork unitOfWork,
    IUserRepository userRepository
) : IRequestHandler<RemoveUserRequest, RemoveUserResponse>
{
    public async Task<RemoveUserResponse> Handle(RemoveUserRequest request, CancellationToken cancellationToken = default)
    {
        UserDto userDto = await authService.GetCurrentUserAsync(cancellationToken);

        User user = await userRepository.FindOneOrThrowAsync(
            userDto.Id,
            cancellationToken
        );

        IEnumerable<Author> authors = await authorRepository.FindManyAsync(
            author => author.UserId == userDto.Id,
            cancellationToken
        );

        foreach (Author author in authors)
        {
            await authorRepository.RemoveSoftAsync(author, cancellationToken);
        }

        await userRepository.RemoveSoftAsync(user, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        return new RemoveUserResponse();
    }
}

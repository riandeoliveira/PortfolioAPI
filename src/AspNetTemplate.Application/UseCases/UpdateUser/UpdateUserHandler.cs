using AspNetTemplate.Domain.Entities;
using AspNetTemplate.Domain.Enums;
using AspNetTemplate.Domain.Exceptions;
using AspNetTemplate.Domain.Interfaces;
using AspNetTemplate.Infrastructure.Tools;

namespace AspNetTemplate.Application.UseCases.UpdateUser;

public sealed class UpdateUserHandler(
    IUnitOfWork unitOfWork,
    IUserRepository userRepository
) : IRequestHandler<UpdateUserRequest>
{
    public async Task Handle(UpdateUserRequest request, CancellationToken cancellationToken = default)
    {
        User user = await userRepository.FindAuthenticatedOrThrowAsync(cancellationToken);

        bool emailAlreadyExists = await userRepository.ExistAsync(
            x => x.Id != user.Id &&
            x.Email == request.Email,
            cancellationToken
        );

        if (emailAlreadyExists) throw new ConflictException(Message.EmailAlreadyExists);

        string hashedPassword = PasswordTool.Hash(request.Password);

        user.Email = request.Email;
        user.Password = hashedPassword;

        await userRepository.UpdateAsync(user, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}

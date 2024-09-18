using AspNetTemplate.Domain.Dtos;
using AspNetTemplate.Domain.Entities;
using AspNetTemplate.Domain.Interfaces;
using AspNetTemplate.Infrastructure.Tools;

namespace AspNetTemplate.Application.UseCases.ResetUserPassword;

public sealed class ResetUserPasswordHandler(
    IAuthService authService,
    IUnitOfWork unitOfWork,
    IUserRepository userRepository
) : IRequestHandler<ResetUserPasswordRequest, ResetUserPasswordResponse>
{
    public async Task<ResetUserPasswordResponse> Handle(ResetUserPasswordRequest request, CancellationToken cancellationToken = default)
    {
        UserDto userDto = await authService.GetCurrentUserAsync(cancellationToken);
        
        User user = await userRepository.FindOneOrThrowAsync(userDto.Id, cancellationToken);

        string hashedPassword = PasswordTool.Hash(request.Password);

        user.Password = hashedPassword;

        await userRepository.UpdateAsync(user, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        return new ResetUserPasswordResponse();
    }
}

using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces;
using Portfolio.Infrastructure.Extensions;
using Portfolio.Infrastructure.Tools;

namespace Portfolio.Application.UseCases.ResetUserPassword;

public sealed class ResetUserPasswordHandler(
    IAuthService authService,
    IUserRepository userRepository,
    ResetUserPasswordValidator validator
) : IRequestHandler<ResetUserPasswordRequest, ResetUserPasswordResponse>
{
    public async Task<ResetUserPasswordResponse> Handle(ResetUserPasswordRequest request, CancellationToken cancellationToken = default)
    {
        await validator.ValidateOrThrowAsync(request, cancellationToken);

        User user = await userRepository.FindOneOrThrowAsync(authService.GetLoggedInUserId(), cancellationToken);

        string hashedPassword = PasswordTool.Hash(request.Password.Trim());

        user.Password = hashedPassword;

        await userRepository.UpdateAsync(user, cancellationToken);
        await userRepository.SaveChangesAsync(cancellationToken);

        return new ResetUserPasswordResponse();
    }
}

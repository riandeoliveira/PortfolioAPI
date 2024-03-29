using Mapster;

using Portfolio.Domain.Constants;
using Portfolio.Domain.Dtos;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Enums;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.UseCases.ForgotUserPassword;

public sealed class ForgotUserPasswordHandler(
    IAuthService authService,
    IMailService mailService,
    IUserRepository userRepository
) : IRequestHandler<ForgotUserPasswordRequest, ForgotUserPasswordResponse>
{
    public async Task<ForgotUserPasswordResponse> Handle(ForgotUserPasswordRequest request, CancellationToken cancellationToken = default)
    {
        User user = await userRepository.FindOneOrThrowAsync(
            user => user.Email == request.Email,
            cancellationToken
        );

        string token = authService.GenerateToken(user.Adapt<UserDto>());

        ForgotUserPasswordViewModel viewModel = new(user.Email, token, EnvironmentVariables.CLIENT_URL);
        MailSenderDto mailSender = new(
            user.Email,
            LocalizationMessages.PasswordResetRequest,
            "ForgotUserPassword",
            viewModel
        );

        await mailService.SendMailAsync(mailSender, cancellationToken);

        return new ForgotUserPasswordResponse();
    }
}

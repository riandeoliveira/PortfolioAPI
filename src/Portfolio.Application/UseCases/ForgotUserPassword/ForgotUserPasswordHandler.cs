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

        TokenDto tokenDto = await authService.GenerateTokenDataAsync(user.Adapt<UserDto>(), cancellationToken);

        ForgotUserPasswordViewModel viewModel = new(user.Email, tokenDto.AccessToken, EnvironmentVariables.CLIENT_URL);

        MailSenderDto mailSenderDto = new(
            Recipient: user.Email,
            Subject: LocalizationMessages.PasswordResetRequest,
            ViewName: "ForgotUserPassword",
            ViewModel: viewModel
        );

        await mailService.SendMailAsync(mailSenderDto, cancellationToken);

        return new ForgotUserPasswordResponse();
    }
}

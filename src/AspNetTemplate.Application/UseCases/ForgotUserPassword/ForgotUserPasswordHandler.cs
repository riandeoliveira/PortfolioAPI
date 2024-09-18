using Mapster;

using AspNetTemplate.Domain.Constants;
using AspNetTemplate.Domain.Dtos;
using AspNetTemplate.Domain.Entities;
using AspNetTemplate.Domain.Enums;
using AspNetTemplate.Domain.Interfaces;

namespace AspNetTemplate.Application.UseCases.ForgotUserPassword;

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
            Subject: Message.PasswordResetRequest,
            ViewName: "ForgotUserPassword",
            ViewModel: viewModel
        );

        await mailService.SendMailAsync(mailSenderDto, cancellationToken);

        return new ForgotUserPasswordResponse();
    }
}

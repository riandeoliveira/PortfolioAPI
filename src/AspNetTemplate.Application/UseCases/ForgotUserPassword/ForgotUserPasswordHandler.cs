using AspNetTemplate.Application.Interfaces;
using AspNetTemplate.Domain.Constants;
using AspNetTemplate.Domain.Entities;
using AspNetTemplate.Domain.Enums;
using AspNetTemplate.Infra.Data.Dtos;
using AspNetTemplate.Infra.Data.Interfaces;

namespace AspNetTemplate.Application.UseCases.ForgotUserPassword;

public sealed class ForgotUserPasswordHandler(
    IAuthService authService,
    IMailService mailService,
    IUserRepository userRepository
) : IRequestHandler<ForgotUserPasswordRequest>
{
    public async Task Handle(ForgotUserPasswordRequest request, CancellationToken cancellationToken = default)
    {
        User user = await userRepository.FindOneOrThrowAsync(
            x => x.Email == request.Email,
            cancellationToken
        );

        JwtTokenDto jwtTokenDto = authService.CreateJwtTokenData(user.Id);

        ForgotUserPasswordViewModel viewModel = new(
            user.Email,
            jwtTokenDto.AccessToken.Value,
            EnvironmentVariables.CLIENT_URL
        );

        ViewDto viewDto = new("ForgotUserPassword", viewModel);

        MailSenderDto mailSenderDto = new(
            Recipient: user.Email,
            Subject: Message.PasswordResetRequest,
            viewDto
        );

        await mailService.SendMailAsync(mailSenderDto, cancellationToken);
    }
}

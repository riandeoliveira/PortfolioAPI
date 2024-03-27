using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

using Portfolio.Domain.Constants;
using Portfolio.Domain.Dtos;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces;
using Portfolio.Infrastructure.Extensions;

namespace Portfolio.Application.UseCases.ForgotUserPassword;

public sealed class ForgotUserPasswordHandler(
    ForgotUserPasswordValidator validator,
    IAuthService authService,
    ILocalizationService localizationService,
    IMailService mailService,
    IUserRepository userRepository
) : IRequestHandler<ForgotUserPasswordRequest, ForgotUserPasswordResponse>
{
    public async Task<ForgotUserPasswordResponse> Handle(ForgotUserPasswordRequest request, CancellationToken cancellationToken = default)
    {
        await validator.ValidateOrThrowAsync(request, cancellationToken);

        User user = await userRepository.FindOneOrThrowAsync(
            user => user.Email == request.Email,
            cancellationToken
        );

        string token = authService.GenerateToken(user);
        string? culture = localizationService.GetCultureName();

        ForgotUserPasswordViewModel viewModel = new(user.Email, token, EnvironmentVariables.CLIENT_URL);
        MailSenderDto mailSender = new(
            "to@example.com",
            "Hello world",
            $"User/Mail.{culture}.cshtml",
            viewModel
        );

        await mailService.SendMailAsync(mailSender, cancellationToken);

        return new ForgotUserPasswordResponse(token);
    }
}

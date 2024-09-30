using AspNetTemplate.Domain.Constants;
using AspNetTemplate.Infra.Data.Dtos;
using AspNetTemplate.Infra.Data.Interfaces;
using AspNetTemplate.Infra.Data.Utilities;

using Razor.Templating.Core;

using System.Net;
using System.Net.Mail;

namespace AspNetTemplate.Infra.Common.Services;

public sealed class MailService : IMailService
{
    public async Task SendMailAsync(MailSenderDto mailSender, CancellationToken cancellationToken = default)
    {
        SmtpClient client = new()
        {
            Credentials = new NetworkCredential(EnvironmentVariables.MAIL_USERNAME, EnvironmentVariables.MAIL_PASSWORD),
            EnableSsl = true,
            Host = EnvironmentVariables.MAIL_HOST,
            Port = int.Parse(EnvironmentVariables.MAIL_PORT)
        };

        string? cultureName = LocalizationUtility.GetCultureName();

        string? html = await RazorTemplateEngine.RenderAsync(
            $"/Views/{cultureName}/{mailSender.ViewDto.Name}.cshtml",
            mailSender.ViewDto.Model
        );

        MailMessage mailMessage = new()
        {
            Body = html,
            From = new MailAddress(EnvironmentVariables.MAIL_SENDER),
            IsBodyHtml = true,
            Subject = LocalizationUtility.GetMessage(mailSender.Subject),
        };

        mailMessage.To.Add(mailSender.Recipient);

        await client.SendMailAsync(mailMessage, cancellationToken);
    }
}

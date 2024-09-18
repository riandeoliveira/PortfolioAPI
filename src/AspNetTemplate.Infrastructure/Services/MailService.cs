using System.Net;
using System.Net.Mail;

using AspNetTemplate.Domain.Constants;
using AspNetTemplate.Domain.Dtos;
using AspNetTemplate.Domain.Interfaces;
using AspNetTemplate.Domain.Services;

using Razor.Templating.Core;

namespace AspNetTemplate.Infrastructure.Services;

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

        string? culture = LocalizationService.GetCultureName();
        string? html = await RazorTemplateEngine.RenderAsync($"/Views/{culture}/{mailSender.ViewName}.cshtml", mailSender.ViewModel);

        MailMessage mailMessage = new()
        {
            Body = html,
            From = new MailAddress(EnvironmentVariables.MAIL_SENDER),
            IsBodyHtml = true,
            Subject = LocalizationService.GetMessage(mailSender.Subject),
        };

        mailMessage.To.Add(mailSender.Recipient);

        await client.SendMailAsync(mailMessage, cancellationToken);
    }
}

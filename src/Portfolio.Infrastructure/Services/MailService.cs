using System.Net;
using System.Net.Mail;

using Portfolio.Domain.Constants;
using Portfolio.Domain.Dtos;
using Portfolio.Domain.Interfaces;

using Razor.Templating.Core;

namespace Portfolio.Infrastructure.Services;

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

        string? html = await RazorTemplateEngine.RenderAsync($"/Views/{mailSender.ViewPath}", mailSender.ViewModel);

        MailMessage mailMessage = new()
        {
            Body = html,
            From = new MailAddress(EnvironmentVariables.MAIL_SENDER),
            IsBodyHtml = true,
            Subject = mailSender.Subject,
        };

        mailMessage.To.Add(mailSender.Recipient);

        await client.SendMailAsync(mailMessage, cancellationToken);
    }
}

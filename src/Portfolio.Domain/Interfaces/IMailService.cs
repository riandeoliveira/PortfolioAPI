using Portfolio.Domain.Dtos;

namespace Portfolio.Domain.Interfaces;

public interface IMailService
{
    Task SendMailAsync(MailSenderDto mailSender, CancellationToken cancellationToken = default);
}

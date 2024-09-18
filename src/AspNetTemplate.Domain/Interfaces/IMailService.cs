using AspNetTemplate.Domain.Dtos;

namespace AspNetTemplate.Domain.Interfaces;

public interface IMailService
{
    Task SendMailAsync(MailSenderDto mailSender, CancellationToken cancellationToken = default);
}

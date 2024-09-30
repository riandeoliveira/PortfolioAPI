using AspNetTemplate.Infra.Data.Dtos;

namespace AspNetTemplate.Infra.Data.Interfaces;

public interface IMailService
{
    Task SendMailAsync(MailSenderDto mailSender, CancellationToken cancellationToken = default);
}

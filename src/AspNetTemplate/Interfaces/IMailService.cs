using AspNetTemplate.Dtos;

namespace AspNetTemplate.Interfaces;

public interface IMailService
{
    public Task SendMailAsync(MailSenderDto mailSender, CancellationToken cancellationToken);
}

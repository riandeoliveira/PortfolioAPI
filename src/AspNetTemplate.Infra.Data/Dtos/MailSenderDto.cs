using AspNetTemplate.Domain.Enums;

namespace AspNetTemplate.Infra.Data.Dtos;

public sealed record MailSenderDto(
    string Recipient,
    Message Subject,
    ViewDto ViewDto
);

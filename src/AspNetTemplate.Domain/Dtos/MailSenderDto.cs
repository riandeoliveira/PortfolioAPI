using AspNetTemplate.Domain.Enums;

namespace AspNetTemplate.Domain.Dtos;

public sealed record MailSenderDto(
    string Recipient,
    Message Subject,
    string? ViewName,
    object? ViewModel
);

using Portfolio.Domain.Enums;

namespace Portfolio.Domain.Dtos;

public sealed record MailSenderDto(
    string Recipient,
    Message Subject,
    string? ViewName,
    object? ViewModel
);

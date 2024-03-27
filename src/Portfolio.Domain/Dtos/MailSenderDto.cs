using Portfolio.Domain.Enums;

namespace Portfolio.Domain.Dtos;

public sealed record MailSenderDto(
    string Recipient,
    LocalizationMessages Subject,
    string? ViewName,
    object? ViewModel
);

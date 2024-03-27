namespace Portfolio.Domain.Dtos;

public sealed record MailSenderDto(
    string Recipient,
    string? Subject,
    string? ViewPath,
    object? ViewModel
);

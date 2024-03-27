namespace Portfolio.Application.UseCases.ForgotUserPassword;

public sealed record ForgotUserPasswordViewModel(string Email, string Token, string ClientUrl);

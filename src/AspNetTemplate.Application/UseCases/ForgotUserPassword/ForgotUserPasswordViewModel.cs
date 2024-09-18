namespace AspNetTemplate.Application.UseCases.ForgotUserPassword;

public sealed record ForgotUserPasswordViewModel(string Email, string AccessToken, string ClientUrl);

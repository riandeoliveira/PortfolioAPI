using AspNetTemplate.Interfaces;
using AspNetTemplate.Repositories;
using AspNetTemplate.SeedWork;
using AspNetTemplate.Services;

namespace AspNetTemplate.Extensions;

public static class DIExtensions
{
    public static WebApplicationBuilder ConfigureDI(this WebApplicationBuilder builder)
    {
        builder
            .Services.AddScoped<IAuthService, AuthService>()
            .AddScoped<II18nService, I18nService>()
            .AddScoped<IMailService, MailService>()
            .AddScoped<IPasswordResetTokenRepository, PasswordResetTokenRepository>()
            .AddScoped<IPasswordService, PasswordService>()
            .AddScoped<IPersonalRefreshTokenRepository, PersonalRefreshTokenRepository>()
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<IUserRepository, UserRepository>();

        return builder;
    }
}

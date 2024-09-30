using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;

using System.Globalization;

namespace AspNetTemplate.Api.Extensions;

internal static class LocalizationExtension
{
    internal static WebApplicationBuilder ConfigureLocalization(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddLocalization(options => options.ResourcesPath = "Resources")
            .Configure<RequestLocalizationOptions>(options =>
            {
                List<CultureInfo> supportedCultures = [new("pt-BR"), new("en-US")];

                options.DefaultRequestCulture = new RequestCulture("pt-BR", "pt-BR");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

        return builder;
    }

    internal static WebApplication UseLocalization(this WebApplication app)
    {
        RequestLocalizationOptions options = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value;

        app.UseRequestLocalization(options);

        return app;
    }
}

using System.Globalization;

using Microsoft.AspNetCore.Localization;

namespace Portfolio.Api.Configurations;

public static class LocalizationConfiguration
{
    public static WebApplicationBuilder ConfigureLocalization(this WebApplicationBuilder builder)
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
}

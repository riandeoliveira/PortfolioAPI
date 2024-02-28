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
                var supportedCultures = new List<CultureInfo>
                {
                    new("pt-BR"),
                    new("en-US")
                };

                options.DefaultRequestCulture = new RequestCulture(culture: "pt-BR", uiCulture: "pt-BR");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

        return builder;
    }
}

using Microsoft.Extensions.Options;

using Serilog;

namespace Portfolio.WebApi.Extensions;

internal static class ApplicationExtension
{
    internal static WebApplication ConfigureApplication(this WebApplication application)
    {
        if (application.Environment.IsDevelopment())
        {
            application.UseSwagger();
            application.UseSwaggerUI();
        }

        application.UseRequestLocalization(application.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
        application.UseStatusCodePages();
        application.UseExceptionHandler();
        application.UseSerilogRequestLogging();
        application.UseHttpsRedirection();
        application.UseAuthentication();
        application.UseAuthorization();
        application.MapControllers();

        return application;
    }
}

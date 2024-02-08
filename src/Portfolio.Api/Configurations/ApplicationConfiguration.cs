using Serilog;

namespace Portfolio.Api.Configurations;

public static class ApplicationConfiguration
{
    public static WebApplication ConfigureApplication(this WebApplication application)
    {
        if (application.Environment.IsDevelopment())
        {
            application.UseSwagger();
            application.UseSwaggerUI();
        }

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

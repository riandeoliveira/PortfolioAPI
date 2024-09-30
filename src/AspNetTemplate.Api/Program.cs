using AspNetTemplate.Api.Extensions;

await WebApplication.CreateBuilder(args)
    .ConfigureDbContext()
    .ConfigureDependencies()
    .ConfigureDocumentation()
    .ConfigureEnvironment()
    .ConfigureHealthCheck()
    .ConfigureHttp()
    .ConfigureJwtAuthentication()
    .ConfigureLocalization()
    .ConfigureLogging()
    .ConfigureRateLimit()

    .Build()

    .UseDocumentation()
    .UseHealthCheck()
    .UseHttp()
    .UseJwtAuthentication()
    .UseLocalization()
    .UseLogging()
    .UseRateLimit()

    .RunAsync();

public partial class Program;

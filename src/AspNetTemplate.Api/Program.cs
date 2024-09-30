using AspNetTemplate.Api.Extensions;

await WebApplication.CreateBuilder(args)
    .ConfigureDbContext()
    .ConfigureDependencies()
    .ConfigureDocumentation()
    .ConfigureEnvironment()
    .ConfigureHealthCheck()
    .ConfigureLocalization()
    .ConfigureHttp()
    .ConfigureJwtAuthentication()
    .ConfigureLogging()
    .ConfigureRateLimit()

    .Build()

    .UseDocumentation()
    .UseHealthCheck()
    .UseLocalization()
    .UseHttp()
    .UseJwtAuthentication()
    .UseLogging()
    .UseRateLimit()

    .RunAsync();

public partial class Program;

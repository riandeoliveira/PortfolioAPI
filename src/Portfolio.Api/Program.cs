using Portfolio.Api.Configurations;
using Portfolio.Users.Environments;

DotEnvExtension.Load();

await WebApplication.CreateBuilder(args)
    .ConfigureAuthentication()
    .ConfigureContext()
    .ConfigureControllers()
    .ConfigureDependencies()
    .ConfigureDocumentation()
    .ConfigureLocalization()
    .Build()
    .ConfigureApplication()
    .RunAsync();

public partial class Program;

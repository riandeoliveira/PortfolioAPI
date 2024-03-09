using Portfolio.Api.Configurations;

using SharpDotEnv;

DotEnv.Config();

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

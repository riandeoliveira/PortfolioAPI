using AspNetTemplate.WebApi.Extensions;

await WebApplication.CreateBuilder(args)
    .ConfigureEnvironment()
    .ConfigureContext()
    .ConfigureDependencies()
    .ConfigureLocalization()
    .ConfigureAuthentication()
    .ConfigureControllers()
    .ConfigureDocumentation()

    .Build()
    .ConfigureApplication()
    .RunAsync();

public partial class Program;

using Portfolio.Api.Configurations;

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

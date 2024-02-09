using Serilog;
using Portfolio.Entities.Context;
using Microsoft.EntityFrameworkCore;
using Portfolio.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .ConfigureAuthentication(builder.Configuration)
    .ConfigureControllers()
    .ConfigureDependencies()
    .ConfigureDocumentation();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddProblemDetails();

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

builder
    .Build()
    .ConfigureApplication()
    .Run();

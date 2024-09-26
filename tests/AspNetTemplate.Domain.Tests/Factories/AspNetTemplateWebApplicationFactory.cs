using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using AspNetTemplate.Infrastructure.Contexts;

using Testcontainers.PostgreSql;

namespace AspNetTemplate.Domain.Tests.Factories;

public sealed class AspNetTemplateWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .WithPassword("postgres")
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder
            .UseEnvironment("Production")
            .ConfigureServices((context, services) =>
            {
                Type descriptorType = typeof(DbContextOptions<ApplicationDbContext>);

                ServiceDescriptor? descriptor = services.SingleOrDefault(
                    serviceDescriptor => serviceDescriptor.ServiceType == descriptorType
                );

                if (descriptor is not null) services.Remove(descriptor);

                string connectionString = _dbContainer.GetConnectionString();

                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention()
                );
            });
    }

    public async Task InitializeAsync()
    {
        await StartContainer();

        ApplicationDbContext context = Services.GetRequiredService<ApplicationDbContext>();

        await context.Database.EnsureCreatedAsync();
        await context.Database.OpenConnectionAsync();
    }

    public new async Task DisposeAsync()
    {
        await StopContainer();
    }

    private async Task StartContainer()
    {
        await _dbContainer.StartAsync();
    }

    private async Task StopContainer()
    {
        await _dbContainer.StopAsync();
    }
}

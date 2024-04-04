using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Portfolio.Infrastructure.Contexts;

using Testcontainers.PostgreSql;

namespace Portfolio.WebApi.IntegrationTests.Factories;

public sealed class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
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
                Type descriptorType = typeof(DbContextOptions<DatabaseContext>);

                ServiceDescriptor? descriptor = services.SingleOrDefault(
                    serviceDescriptor => serviceDescriptor.ServiceType == descriptorType
                );

                if (descriptor is not null) services.Remove(descriptor);

                string connectionString = _dbContainer.GetConnectionString();

                services.AddDbContext<DatabaseContext>(options =>
                    options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention()
                );
            });
    }

    public new async Task DisposeAsync()
    {
        await StopContainer();
    }

    public async Task InitializeAsync()
    {
        await StartContainer();

        DatabaseContext context = Services.GetRequiredService<DatabaseContext>();

        await context.Database.EnsureCreatedAsync();
        await context.Database.OpenConnectionAsync();
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

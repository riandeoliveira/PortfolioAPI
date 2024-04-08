using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Portfolio.Domain.Entities;
using Portfolio.Domain.Tests.Fixtures;
using Portfolio.Infrastructure.Contexts;
using Portfolio.Infrastructure.Tools;

using Testcontainers.PostgreSql;

namespace Portfolio.Domain.Tests.Factories;

public sealed class PortfolioWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
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

    public async Task InitializeAsync()
    {
        await StartContainer();

        DatabaseContext context = Services.GetRequiredService<DatabaseContext>();

        await context.Database.EnsureCreatedAsync();
        await context.Database.OpenConnectionAsync();

        await PopulateDatabase(context);
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

    private static async Task PopulateDatabase(DatabaseContext databaseContext)
    {
        User user = DatabaseFixture.User;

        user.Password = PasswordTool.Hash(DatabaseFixture.User.Password);

        await databaseContext.Authors.AddAsync(DatabaseFixture.Author);
        await databaseContext.Users.AddAsync(user);

        await databaseContext.SaveChangesAsync();
    }
}

using Microsoft.EntityFrameworkCore;

using Portfolio.Domain.Context;

namespace Portfolio.Api.Configurations;

public static class ContextConfiguration
{
    public static WebApplicationBuilder ConfigureContext(this WebApplicationBuilder builder)
    {
        var database = new
        {
            host = Environment.GetEnvironmentVariable("DATABASE_HOST"),
            name = Environment.GetEnvironmentVariable("DATABASE_NAME"),
            password = Environment.GetEnvironmentVariable("DATABASE_PASSWORD"),
            port = Environment.GetEnvironmentVariable("DATABASE_PORT"),
            user = Environment.GetEnvironmentVariable("DATABASE_USER")
        };

        string connectionString = $"Server={database.host};Port={database.port};Database={database.name};User Id={database.user};Password={database.password}";

        builder.Services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(connectionString));

        return builder;
    }
}

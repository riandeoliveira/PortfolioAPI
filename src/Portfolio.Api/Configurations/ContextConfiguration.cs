using Microsoft.EntityFrameworkCore;

using Portfolio.Domain.Context;
using Portfolio.Utils.Constants;

namespace Portfolio.Api.Configurations;

public static class ContextConfiguration
{
    public static WebApplicationBuilder ConfigureContext(this WebApplicationBuilder builder)
    {
        string connectionString =
        $@"
            Server={EnvironmentVariables.DATABASE_HOST};
            Port={EnvironmentVariables.DATABASE_PORT};
            Database={EnvironmentVariables.DATABASE_NAME};
            User Id={EnvironmentVariables.DATABASE_USER};
            Password={EnvironmentVariables.DATABASE_PASSWORD}
        ";

        builder.Services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(connectionString));

        return builder;
    }
}

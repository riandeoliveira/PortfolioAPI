using Microsoft.EntityFrameworkCore;

using Portfolio.Domain.Context;

namespace Portfolio.Api.Configurations;

public static class ContextConfiguration
{
    public static WebApplicationBuilder ConfigureContext(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlite(connectionString));

        return builder;
    }
}

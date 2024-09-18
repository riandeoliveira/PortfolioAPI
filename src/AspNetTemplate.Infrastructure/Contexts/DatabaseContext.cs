using Microsoft.EntityFrameworkCore;

using AspNetTemplate.Domain.Entities;

namespace AspNetTemplate.Infrastructure.Contexts;

public sealed class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();

    static DatabaseContext() => AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
}

using Microsoft.EntityFrameworkCore;

using Portfolio.Domain.Entities;

namespace Portfolio.Infrastructure.Contexts;

public sealed class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<Author> Authors => Set<Author>();
    public DbSet<User> Users => Set<User>();

    static DatabaseContext() => AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
}

using Microsoft.EntityFrameworkCore;

using Portfolio.Entities.Mapping;

namespace Portfolio.Entities.Context;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<Author> Authors => Set<Author>();

    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AuthorMap());
        modelBuilder.ApplyConfiguration(new UserMap());
    }
}

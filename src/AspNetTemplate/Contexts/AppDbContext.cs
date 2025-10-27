using Microsoft.EntityFrameworkCore;
using AspNetTemplate.Entities;

namespace AspNetTemplate.Contexts;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<PasswordResetToken> PasswordResetTokens => Set<PasswordResetToken>();

    public DbSet<PersonalRefreshToken> PersonalRefreshTokens => Set<PersonalRefreshToken>();

    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PasswordResetToken>().HasIndex(x => x.Value).IsUnique();

        modelBuilder.Entity<PersonalRefreshToken>().HasIndex(x => x.Value).IsUnique();

        modelBuilder.Entity<User>().HasIndex(x => x.Email).IsUnique();

        modelBuilder.Entity<User>().HasIndex(x => x.Username).IsUnique();
    }
}

using AspNetTemplate.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace AspNetTemplate.Infra.Data.Contexts;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<PersonalRefreshToken> PersonalRefreshTokens => Set<PersonalRefreshToken>();
    public DbSet<User> Users => Set<User>();

    // static ApplicationDbContext() => AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
}

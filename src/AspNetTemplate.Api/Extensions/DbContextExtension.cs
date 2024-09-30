using AspNetTemplate.Domain.Constants;
using AspNetTemplate.Infra.Data.Contexts;

using Microsoft.EntityFrameworkCore;

namespace AspNetTemplate.Api.Extensions;

internal static class DbContextExtension
{
    internal static WebApplicationBuilder ConfigureDbContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(DatabaseAccess.CONNECTION_STRING));

        return builder;
    }
}

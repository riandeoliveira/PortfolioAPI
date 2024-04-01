using Microsoft.EntityFrameworkCore;

using Portfolio.Domain.Constants;
using Portfolio.Infrastructure.Contexts;

namespace Portfolio.WebApi.Extensions;

internal static class ContextExtension
{
    internal static WebApplicationBuilder ConfigureContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(Database.CONNECTION_STRING));

        return builder;
    }
}

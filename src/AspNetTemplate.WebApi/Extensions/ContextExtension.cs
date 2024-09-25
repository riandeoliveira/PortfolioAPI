using Microsoft.EntityFrameworkCore;

using AspNetTemplate.Domain.Constants;
using AspNetTemplate.Infrastructure.Contexts;

namespace AspNetTemplate.WebApi.Extensions;

internal static class ContextExtension
{
    internal static WebApplicationBuilder ConfigureContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(Database.CONNECTION_STRING));

        return builder;
    }
}

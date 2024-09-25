using System.Security.Claims;

using AspNetTemplate.Domain.Interfaces;

using Microsoft.AspNetCore.Http;

namespace AspNetTemplate.Infrastructure.Middlewares;

public class CookieAuthenticationMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context, IAuthService authService)
    {
        Guid? userId = authService.FindAuthenticatedUserId();

        if (userId.HasValue)
        {
            List<Claim> claims =
            [
                new(ClaimTypes.NameIdentifier, userId.Value.ToString())
            ];

            ClaimsIdentity identity = new(claims, "Cookies");
            ClaimsPrincipal principal = new(identity);

            context.User = principal;
        }

        await _next(context);
    }
}

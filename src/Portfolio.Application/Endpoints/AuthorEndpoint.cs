using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Application.Endpoints;

[ApiController]
[Authorize]
[Produces("application/json", "application/problem+json")]
[Route("api/author")]
public abstract class AuthorEndpoint : Controller
{
}

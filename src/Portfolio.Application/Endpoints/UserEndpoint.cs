using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Application.Endpoints;

[ApiController]
[Produces("application/json", "application/problem+json")]
[Route("api/user")]
public abstract class UserEndpoint : Controller
{
}

using Microsoft.AspNetCore.Mvc;

namespace AspNetTemplate.Application.Endpoints;

[ApiController]
[Produces("application/json", "application/problem+json")]
public abstract class BaseEndpoint : Controller
{
}

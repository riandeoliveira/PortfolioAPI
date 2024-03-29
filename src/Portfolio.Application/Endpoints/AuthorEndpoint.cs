using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Application.Endpoints;

[Authorize]
[Route("api/author")]
public abstract class AuthorEndpoint : BaseEndpoint
{
}

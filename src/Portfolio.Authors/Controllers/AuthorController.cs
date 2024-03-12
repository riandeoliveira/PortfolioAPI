using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Authors.Controllers;

[ApiController]
[Authorize]
[Produces("application/json")]
[Route("api/author")]
public sealed partial class AuthorController(IMediator mediator) : ControllerBase
{
}

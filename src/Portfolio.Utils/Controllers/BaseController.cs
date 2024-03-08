using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Utils.Controllers;

[ApiController]
[Produces("application/json")]
public abstract class BaseController(IMediator mediator) : ControllerBase
{
    protected readonly IMediator _mediator = mediator;
}

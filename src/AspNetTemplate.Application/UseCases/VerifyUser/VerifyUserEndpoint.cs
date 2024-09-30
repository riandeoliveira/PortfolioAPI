using AspNetTemplate.Infra.Data.Dtos;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

using Swashbuckle.AspNetCore.Annotations;

namespace AspNetTemplate.Application.UseCases.VerifyUser;

[ApiController]
[EnableRateLimiting("Fixed")]
[Produces("application/json", "application/problem+json")]
[Route("api/user")]
public sealed class VerifyUserEndpoint(IMediator mediator) : Controller
{
    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetailsDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetailsDto))]
    [ProducesResponseType(StatusCodes.Status429TooManyRequests, Type = typeof(ProblemDetailsDto))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetailsDto))]
    [SwaggerOperation(
        Description = "",
        OperationId = "VerifyUser",
        Tags = ["User"]
    )]
    public async Task<IActionResult> Handle(CancellationToken cancellationToken = default)
    {
        await mediator.Send(new VerifyUserRequest(), cancellationToken);

        return NoContent();
    }
}

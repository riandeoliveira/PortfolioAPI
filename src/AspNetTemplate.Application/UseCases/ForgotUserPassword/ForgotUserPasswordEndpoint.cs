using AspNetTemplate.Infra.Data.Dtos;

using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

using Swashbuckle.AspNetCore.Annotations;

namespace AspNetTemplate.Application.UseCases.ForgotUserPassword;

[ApiController]
[EnableRateLimiting("Fixed")]
[Produces("application/json", "application/problem+json")]
[Route("api/user")]
public sealed class ForgotUserEndpoint(IMediator mediator) : Controller
{
    [HttpPost("forgot-password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetailsDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetailsDto))]
    [ProducesResponseType(StatusCodes.Status429TooManyRequests, Type = typeof(ProblemDetailsDto))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetailsDto))]
    [SwaggerOperation(
        Description = "",
        OperationId = "ForgotUserPassword",
        Tags = ["User"]
    )]
    public async Task<IActionResult> Handle([FromBody] ForgotUserPasswordRequest request, CancellationToken cancellationToken = default)
    {
        await mediator.Send(request, cancellationToken);

        return NoContent();
    }
}

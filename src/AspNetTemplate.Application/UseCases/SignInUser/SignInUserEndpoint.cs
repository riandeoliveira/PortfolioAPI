using AspNetTemplate.Infra.Data.Dtos;

using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace AspNetTemplate.Application.UseCases.SignInUser;

[ApiController]
[EnableRateLimiting("Fixed")]
[Produces("application/json", "application/problem+json")]
[Route("api/user")]
public sealed class SignInUserEndpoint(IMediator mediator) : Controller
{
    [HttpPost("sign-in")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetailsDto))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetailsDto))]
    [ProducesResponseType(StatusCodes.Status429TooManyRequests, Type = typeof(ProblemDetailsDto))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetailsDto))]
    [SwaggerOperation(
        Description = "",
        OperationId = "SignInUser",
        Tags = ["User"]
    )]
    [SwaggerRequestExample(typeof(SignInUserRequest), typeof(SignInUserRequestExample))]
    public async Task<IActionResult> Handle([FromBody] SignInUserRequest request, CancellationToken cancellationToken = default)
    {
        await mediator.Send(request, cancellationToken);

        return NoContent();
    }
}

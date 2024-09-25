using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using AspNetTemplate.Application.Endpoints;

using Swashbuckle.AspNetCore.Annotations;
using AspNetTemplate.Domain.Dtos;

namespace AspNetTemplate.Application.UseCases.ResetUserPassword;

public sealed class ResetUserEndpoint(IMediator mediator) : UserEndpoint
{
    [Authorize]
    [HttpPost("reset-password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetailsDto))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetailsDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetailsDto))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetailsDto))]
    [SwaggerOperation(
        Description = "",
        OperationId = "ResetUserPassword",
        Tags = ["User"]
    )]
    public async Task<IActionResult> Handle([FromBody] ResetUserPasswordRequest request, CancellationToken cancellationToken = default)
    {
        await mediator.Send(request, cancellationToken);

        return NoContent();
    }
}

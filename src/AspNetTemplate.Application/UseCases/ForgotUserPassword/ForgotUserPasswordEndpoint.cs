using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using AspNetTemplate.Application.Endpoints;

using Swashbuckle.AspNetCore.Annotations;
using AspNetTemplate.Domain.Dtos;

namespace AspNetTemplate.Application.UseCases.ForgotUserPassword;

public sealed class ForgotUserEndpoint(IMediator mediator) : UserEndpoint
{
    [HttpPost("forgot-password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetailsDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetailsDto))]
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

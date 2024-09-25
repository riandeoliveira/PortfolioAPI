using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using AspNetTemplate.Application.Endpoints;

using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using AspNetTemplate.Domain.Dtos;

namespace AspNetTemplate.Application.UseCases.UpdateUser;

public sealed class UpdateUserEndpoint(IMediator mediator) : UserEndpoint
{
    [Authorize]
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetailsDto))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetailsDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetailsDto))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ProblemDetailsDto))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetailsDto))]
    [SwaggerOperation(
        Description = "",
        OperationId = "UpdateUser",
        Tags = ["User"]
    )]
    [SwaggerRequestExample(typeof(UpdateUserRequest), typeof(UpdateUserRequestExample))]
    public async Task<IActionResult> Handle([FromBody] UpdateUserRequest request, CancellationToken cancellationToken = default)
    {
        await mediator.Send(request, cancellationToken);

        return NoContent();
    }
}

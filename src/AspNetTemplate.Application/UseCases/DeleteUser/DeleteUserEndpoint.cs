using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using AspNetTemplate.Application.Endpoints;

using Swashbuckle.AspNetCore.Annotations;
using AspNetTemplate.Domain.Dtos;

namespace AspNetTemplate.Application.UseCases.DeleteUser;

public sealed class DeleteUserEndpoint(IMediator mediator) : UserEndpoint
{
    [Authorize]
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetailsDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetailsDto))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetailsDto))]
    [SwaggerOperation(
        Description = "",
        OperationId = "DeleteUser",
        Tags = ["User"]
    )]
    public async Task<IActionResult> Handle(CancellationToken cancellationToken = default)
    {
        await mediator.Send(new DeleteUserRequest(), cancellationToken);

        return NoContent();
    }
}

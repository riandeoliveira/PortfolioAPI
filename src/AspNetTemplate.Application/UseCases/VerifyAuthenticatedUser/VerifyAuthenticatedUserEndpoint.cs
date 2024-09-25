using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using AspNetTemplate.Application.Endpoints;

using Swashbuckle.AspNetCore.Annotations;
using AspNetTemplate.Domain.Dtos;

namespace AspNetTemplate.Application.UseCases.VerifyAuthenticatedUser;

public sealed class VerifyAuthenticatedUserEndpoint(IMediator mediator) : UserEndpoint
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetailsDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetailsDto))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetailsDto))]
    [SwaggerOperation(
        Description = "",
        OperationId = "VerifyAuthenticatedUser",
        Tags = ["User"]
    )]
    public async Task<IActionResult> Handle(CancellationToken cancellationToken = default)
    {
        await mediator.Send(new VerifyAuthenticatedUserRequest(), cancellationToken);

        return NoContent();
    }
}

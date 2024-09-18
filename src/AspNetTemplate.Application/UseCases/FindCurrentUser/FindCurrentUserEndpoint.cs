using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using AspNetTemplate.Application.Endpoints;
using AspNetTemplate.Domain.Dtos;

using Swashbuckle.AspNetCore.Annotations;

namespace AspNetTemplate.Application.UseCases.FindCurrentUser;

public sealed class FindCurrentUserEndpoint(IMediator mediator) : UserEndpoint
{
    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    [SwaggerOperation(
        Description = "",
        OperationId = "FindCurrentUser",
        Tags = ["User"]
    )]
    public async Task<IActionResult> Handle(CancellationToken cancellationToken = default)
    {
        try
        {
            FindCurrentUserResponse response = await mediator.Send(new FindCurrentUserRequest(), cancellationToken);

            return Ok(response.User);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}

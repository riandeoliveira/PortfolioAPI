using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Portfolio.Application.Endpoints;
using Portfolio.Domain.Dtos;

using Swashbuckle.AspNetCore.Annotations;

namespace Portfolio.Application.UseCases.FindCurrentUser;

/// <summary>
/// Endpoint for retrieving the details of the currently logged-in user.
/// </summary>
public sealed class FindCurrentUserEndpoint(IMediator mediator) : UserEndpoint
{
    /// <summary>
    /// Handles the request to retrieve the details of the currently logged-in user.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    [SwaggerOperation(
        Description = "Retrieves the details of the currently logged-in user.",
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

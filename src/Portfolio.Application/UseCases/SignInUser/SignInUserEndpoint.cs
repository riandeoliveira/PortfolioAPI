using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Portfolio.Application.Endpoints;
using Portfolio.Domain.Dtos;

using Swashbuckle.AspNetCore.Annotations;

namespace Portfolio.Application.UseCases.SignInUser;

/// <summary>
/// Endpoint for user sign-in operations.
/// </summary>
public sealed class SignInUserEndpoint(IMediator mediator) : UserEndpoint
{
    /// <summary>
    /// Handles the sign-in request for a user.
    /// </summary>
    /// <param name="request">The sign-in request.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    [HttpPost("sign-in")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    [SwaggerOperation(
        Description = "Authenticates a user and returns a token and user ID.",
        OperationId = "SignInUser",
        Tags = ["User"]
    )]
    public async Task<IActionResult> Handle([FromBody] SignInUserRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            SignInUserResponse response = await mediator.Send(request, cancellationToken);

            return StatusCode(StatusCodes.Status200OK, response.TokenDto);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}

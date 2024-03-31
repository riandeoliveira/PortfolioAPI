using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Portfolio.Application.Endpoints;

using Swashbuckle.AspNetCore.Annotations;

namespace Portfolio.Application.UseCases.ResetUserPassword;

/// <summary>
/// Endpoint for resetting a user's password.
/// </summary>
public sealed class ResetUserEndpoint(IMediator mediator) : UserEndpoint
{
    /// <summary>
    /// Handles the password reset request for a user.
    /// </summary>
    /// <param name="request">The password reset request containing the new password and its confirmation.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    [Authorize]
    [HttpPost("reset-password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    [SwaggerOperation(
        Description = "Resets the user's password with a new password and its confirmation.",
        OperationId = "ResetUserPassword",
        Tags = ["User"]
    )]
    public async Task<IActionResult> Handle([FromBody] ResetUserPasswordRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            await mediator.Send(request, cancellationToken);

            return NoContent();
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}

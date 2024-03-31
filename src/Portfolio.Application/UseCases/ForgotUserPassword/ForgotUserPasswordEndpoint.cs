using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Portfolio.Application.Endpoints;

using Swashbuckle.AspNetCore.Annotations;

namespace Portfolio.Application.UseCases.ForgotUserPassword;

/// <summary>
/// Endpoint for user password recovery operations.
/// </summary>
public sealed class ForgotUserEndpoint(IMediator mediator) : UserEndpoint
{
    /// <summary>
    /// Handles the forgot password request for a user.
    /// </summary>
    /// <param name="request">The forgot password request containing the user's email.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    [HttpPost("forgot-password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    [SwaggerOperation(
        Description = "Initiates the password recovery process for a user by sending a password reset link to the user's email.",
        OperationId = "ForgotUserPassword",
        Tags = ["User"]
    )]
    public async Task<IActionResult> Handle([FromBody] ForgotUserPasswordRequest request, CancellationToken cancellationToken = default)
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

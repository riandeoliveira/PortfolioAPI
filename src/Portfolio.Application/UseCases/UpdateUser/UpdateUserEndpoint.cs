using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Portfolio.Application.Endpoints;

using Swashbuckle.AspNetCore.Annotations;

namespace Portfolio.Application.UseCases.UpdateUser
{
    /// <summary>
    /// Endpoint for updating user account information.
    /// </summary>
    public sealed class UpdateUserEndpoint(IMediator mediator) : UserEndpoint
    {
        /// <summary>
        /// Handles the request to update the authenticated user's account information.
        /// </summary>
        /// <param name="request">The update user request containing the new account information.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        [Authorize]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerOperation(
            Description = "Updates the authenticated user's account information.",
            OperationId = "UpdateUser",
            Tags = ["User"]
        )]
        public async Task<IActionResult> Handle([FromBody] UpdateUserRequest request, CancellationToken cancellationToken = default)
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
}

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Portfolio.Application.Endpoints;

using Swashbuckle.AspNetCore.Annotations;

namespace Portfolio.Application.UseCases.RemoveUser
{
    /// <summary>
    /// Endpoint for removing a user account.
    /// </summary>
    public sealed class RemoveUserEndpoint(IMediator mediator) : UserEndpoint
    {
        /// <summary>
        /// Handles the request to remove a user account.
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        [Authorize]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerOperation(
            Description = "Removes the authenticated user's account.",
            OperationId = "RemoveUser",
            Tags = ["User"]
        )]
        public async Task<IActionResult> Handle(CancellationToken cancellationToken = default)
        {
            try
            {
                await mediator.Send(new RemoveUserRequest(), cancellationToken);

                return NoContent();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}

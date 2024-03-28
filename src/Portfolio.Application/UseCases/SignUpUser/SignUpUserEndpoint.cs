using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Portfolio.Application.Endpoints;

using Swashbuckle.AspNetCore.Annotations;

namespace Portfolio.Application.UseCases.SignUpUser
{
    /// <summary>
    /// Endpoint for user sign-up operations.
    /// </summary>
    public sealed class SignUpUserEndpoint(IMediator mediator) : UserEndpoint
    {
        /// <summary>
        /// Handles the sign-up request for a new user.
        /// </summary>
        /// <param name="request">The sign-up request containing user details.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        [HttpPost("sign-up")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SignUpUserResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerOperation(
            Description = "Authenticates a new user and returns a token and user ID.",
            OperationId = "SignUpUser",
            Tags = ["User"]
        )]
        public async Task<IActionResult> Handle([FromBody] SignUpUserRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                SignUpUserResponse response = await mediator.Send(request, cancellationToken);

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}

using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Portfolio.Application.Endpoints;

using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Portfolio.Application.UseCases.RemoveAuthor;

/// <summary>
/// Endpoint for removing an author by ID.
/// </summary>
public sealed class RemoveAuthorEndpoint(IMediator mediator) : AuthorEndpoint
{
    /// <summary>
    /// Handles the request to remove an author by ID.
    /// </summary>
    /// <param name="id">The ID of the author to remove.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    [SwaggerOperation(
        Description = "Removes an author by ID.",
        OperationId = "RemoveAuthor",
        Tags = ["Author"]
    )]
    [SwaggerRequestExample(typeof(RemoveAuthorRequest), typeof(RemoveAuthorExample))]
    public async Task<IActionResult> Handle([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            await mediator.Send(new RemoveAuthorRequest(id), cancellationToken);

            return NoContent();
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}

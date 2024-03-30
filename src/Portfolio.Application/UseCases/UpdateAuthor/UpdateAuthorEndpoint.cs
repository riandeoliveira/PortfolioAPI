using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Portfolio.Application.Endpoints;

using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Portfolio.Application.UseCases.UpdateAuthor;

/// <summary>
/// Endpoint for updating an author's details.
/// </summary>
public sealed class UpdateAuthorEndpoint(IMediator mediator) : AuthorEndpoint
{
    /// <summary>
    /// Handles the request to update an author's details.
    /// </summary>
    /// <param name="request">The request containing the updated author details.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    [SwaggerOperation(
        Description = "Updates an author's details.",
        OperationId = "UpdateAuthor",
        Tags = ["Author"]
    )]
    [SwaggerRequestExample(typeof(UpdateAuthorRequest), typeof(UpdateAuthorExample))]
    public async Task<IActionResult> Handle([FromBody] UpdateAuthorRequest request, CancellationToken cancellationToken = default)
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

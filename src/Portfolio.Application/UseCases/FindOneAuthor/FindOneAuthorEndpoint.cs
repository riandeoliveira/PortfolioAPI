using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Portfolio.Application.Endpoints;
using Portfolio.Domain.Dtos;

using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Portfolio.Application.UseCases.FindOneAuthor;

/// <summary>
/// Endpoint for finding a single author by ID.
/// </summary>
public sealed class FindOneAuthorEndpoint(IMediator mediator) : AuthorEndpoint
{
    /// <summary>
    /// Handles the request to find a single author by ID.
    /// </summary>
    /// <param name="id">The ID of the author to find.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthorDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    [SwaggerOperation(
        Description = "Finds a single author by ID and returns their details.",
        OperationId = "FindOneAuthor",
        Tags = ["Author"]
    )]
    [SwaggerRequestExample(typeof(FindOneAuthorRequest), typeof(FindOneAuthorExample))]
    public async Task<IActionResult> Handle([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            FindOneAuthorResponse response = await mediator.Send(new FindOneAuthorRequest(id), cancellationToken);

            return Ok(response.Author);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}

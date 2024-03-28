using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Portfolio.Application.Endpoints;
using Portfolio.Domain.Dtos;

using Swashbuckle.AspNetCore.Annotations;

namespace Portfolio.Application.UseCases.CreateAuthor;

/// <summary>
/// Endpoint for creating a new author.
/// </summary>
public sealed class CreateAuthorEndpoint(IMediator mediator) : AuthorEndpoint
{
    /// <summary>
    /// Handles the request to create a new author.
    /// </summary>
    /// <param name="request">The request containing the author's details.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AuthorDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    [SwaggerOperation(
        Description = "Creates a new author and returns the author's details.",
        OperationId = "CreateAuthor",
        Tags = ["Author"]
    )]
    public async Task<IActionResult> Handle([FromBody] CreateAuthorRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            CreateAuthorResponse response = await mediator.Send(request, cancellationToken);

            return StatusCode(StatusCodes.Status201Created, response.Author);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}

using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Portfolio.Application.Endpoints;
using Portfolio.Domain.Dtos;

using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Portfolio.Application.UseCases.FindManyAuthors;

/// <summary>
/// Endpoint for finding multiple authors.
/// </summary>
public sealed class FindManyAuthorsEndpoint(IMediator mediator) : AuthorEndpoint
{
    /// <summary>
    /// Handles the request to find multiple authors.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AuthorDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    [SwaggerOperation(
        Description = "Finds multiple authors and returns their details.",
        OperationId = "FindManyAuthors",
        Tags = ["Author"]
    )]
    [SwaggerRequestExample(typeof(FindManyAuthorsRequest), typeof(FindManyAuthorsExample))]
    public async Task<IActionResult> Handle([FromQuery] FindManyAuthorsRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            FindManyAuthorsResponse response = await mediator.Send(request, cancellationToken);

            return Ok(response);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}

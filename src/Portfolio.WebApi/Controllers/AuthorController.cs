using System.Net;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Portfolio.Application.UseCases.CreateAuthor;
using Portfolio.Application.UseCases.FindManyAuthors;
using Portfolio.Application.UseCases.FindOneAuthor;
using Portfolio.Application.UseCases.RemoveAuthor;
using Portfolio.Application.UseCases.UpdateAuthor;

namespace Portfolio.WebApi.Controllers;

[ApiController]
[Authorize]
[Produces("application/json")]
[Route("api/author")]
public sealed class AuthorController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateAuthorRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            CreateAuthorResponse response = await mediator.Send(request, cancellationToken);
            return StatusCode((int) HttpStatusCode.Created, response.Author);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> FindManyAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            FindManyAuthorsResponse response = await mediator.Send(new FindManyAuthorsRequest(), cancellationToken);

            return Ok(response.Authors);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> FindOneAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
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

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RemoveAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
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

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateAuthorRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            await mediator.Send(request, cancellationToken);

            return Ok();
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}

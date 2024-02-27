using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Portfolio.Authors.Requests;
using Portfolio.Utils.Controllers;

namespace Portfolio.Authors.Controllers;

[Authorize]
[Route("api/author")]
public sealed class AuthorController(IMediator mediator) : BaseController
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateAuthorRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            var author = await _mediator.Send(request, cancellationToken);

            return Ok(author);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RemoveAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            await _mediator.Send(new RemoveAuthorRequest(id), cancellationToken);

            return Ok();
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}

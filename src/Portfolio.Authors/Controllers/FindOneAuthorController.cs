using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Portfolio.Authors.Features.FindOne;

namespace Portfolio.Authors.Controllers;

public sealed partial class AuthorController
{
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> FindAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
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

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Portfolio.Authors.Features.Remove;

namespace Portfolio.Authors.Controllers;

public sealed partial class AuthorController
{
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
}

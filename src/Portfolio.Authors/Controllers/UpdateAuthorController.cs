using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Portfolio.Authors.Features.Update;

namespace Portfolio.Authors.Controllers;

public sealed partial class AuthorController
{
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

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Portfolio.Authors.Features.FindMany;

namespace Portfolio.Authors.Controllers;

public sealed partial class AuthorController
{
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
}

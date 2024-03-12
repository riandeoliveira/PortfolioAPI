using System.Net;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Portfolio.Authors.Features.Create;

namespace Portfolio.Authors.Controllers;

public sealed partial class AuthorController
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
}

using System.Net;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Portfolio.Users.Features.SignIn;

namespace Portfolio.Users.Controllers;

public sealed partial class UserController
{
    [HttpPost("sign-in")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SignInAsync([FromBody] SignInUserRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            SignInUserResponse response = await mediator.Send(request, cancellationToken);

            return StatusCode((int) HttpStatusCode.OK, response);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}

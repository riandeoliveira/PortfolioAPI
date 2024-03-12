using System.Net;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Portfolio.Users.Features.SignUp;

namespace Portfolio.Users.Controllers;

public sealed partial class UserController
{
    [HttpPost("sign-up")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SignUpAsync([FromBody] SignUpUserRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            SignUpUserResponse response = await mediator.Send(request, cancellationToken);

            return StatusCode((int) HttpStatusCode.Created, response);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}

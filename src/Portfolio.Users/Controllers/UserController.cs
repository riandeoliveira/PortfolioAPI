using System.Net;

using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Portfolio.Users.Features.SignIn;
using Portfolio.Users.Features.SignUp;

namespace Portfolio.Users.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/user")]
public sealed class UserController(IMediator mediator) : ControllerBase
{
    // NOTE: RemoveAsync -> excluir conta
    // NOTE: SignInAsync -> iniciar sessão
    // NOTE: SignOutAsync -> encerrar sessão
    // NOTE: SignUpAsync -> cadastrar usuário

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

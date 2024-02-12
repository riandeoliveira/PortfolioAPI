using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Portfolio.Users.Requests;
using Portfolio.Utils.Controllers;

namespace Portfolio.Users.Controllers;

[Route("api/user")]
public sealed class UserController(IMediator mediator) : BaseController
{
    private readonly IMediator _mediator = mediator;

    // LoginAsync -> entrar
    // LogoutAsync -> sair
    // SignInAsync -> cadastrar
    // SignOutAsync -> excluir conta

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> LoginAsync([FromBody] LoginUserRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            var user = await _mediator.Send(request, cancellationToken);

            return Ok(user);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpPost("sign-in")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SignInAsync([FromBody] SignInUserRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            var user = await _mediator.Send(request, cancellationToken);

            return Ok(user);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}

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

    // RemoveAsync -> excluir conta
    // SignInAsync -> iniciar sessão
    // SignOutAsync -> encerrar sessão
    // SignUpAsync -> cadastrar usuário

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

    [HttpPost("sign-up")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SignUpAsync([FromBody] SignUpUserRequest request, CancellationToken cancellationToken = default)
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

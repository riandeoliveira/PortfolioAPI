using Microsoft.AspNetCore.Mvc;

using Portfolio.Users.Interfaces;
using Portfolio.Users.Requests;
using Portfolio.Utils.Controllers;

namespace Portfolio.Users.Controllers;

[Route("api/user")]
public sealed class UserController(IUserService service) : BaseController
{
    private readonly IUserService _service = service;

    // LoginAsync -> entrar
    // LogoutAsync -> sair
    // SignInAsync -> cadastrar
    // SignOutAsync -> excluir conta

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginUserRequest request)
    {
        try
        {
            var user = await _service.LoginAsync(request);

            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> SignInAsync([FromBody] SignInUserRequest request)
    {
        try
        {
            var user = await _service.SignInAsync(request);

            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

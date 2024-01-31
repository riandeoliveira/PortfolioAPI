using Microsoft.AspNetCore.Mvc;

using PortfolioAPI.Entities;
using PortfolioAPI.Services.Dtos;
using PortfolioAPI.Services.Interfaces;

namespace PortfolioAPI.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/user")]
public sealed class UserController(IUserService service) : ControllerBase
{
    private readonly IUserService _service = service;

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateUserDTO dto)
    {
        var user = await _service.CreateAsync(dto);

        return Ok(user);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        await _service.DeleteAsync(id);

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var users = await _service.GetAsync();

        return Ok(users);
    }
}

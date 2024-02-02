using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Portfolio.Authors.Interfaces;
using Portfolio.Authors.Requests;
using Portfolio.Utils.Controllers;

namespace Portfolio.Authors.Controllers;

[Authorize]
[Route("api/author")]
public sealed class AuthorController(IAuthorService service) : BaseController
{
    private readonly IAuthorService _service = service;

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateAuthorRequest request)
    {
        var author = await _service.CreateAsync(request);

        return Ok(author);
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
        var authors = await _service.GetAsync();

        return Ok(authors);
    }
}

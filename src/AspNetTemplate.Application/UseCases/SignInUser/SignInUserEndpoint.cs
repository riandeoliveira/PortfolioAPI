using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using AspNetTemplate.Application.Endpoints;
using AspNetTemplate.Domain.Dtos;

using Swashbuckle.AspNetCore.Annotations;

namespace AspNetTemplate.Application.UseCases.SignInUser;

public sealed class SignInUserEndpoint(IMediator mediator) : UserEndpoint
{
    [HttpPost("sign-in")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    [SwaggerOperation(
        Description = "",
        OperationId = "SignInUser",
        Tags = ["User"]
    )]
    public async Task<IActionResult> Handle([FromBody] SignInUserRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            SignInUserResponse response = await mediator.Send(request, cancellationToken);

            return StatusCode(StatusCodes.Status200OK, response.TokenDto);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}

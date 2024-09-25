using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using AspNetTemplate.Application.Endpoints;

using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using AspNetTemplate.Domain.Dtos;

namespace AspNetTemplate.Application.UseCases.SignInUser;

public sealed class SignInUserEndpoint(IMediator mediator) : UserEndpoint
{
    [HttpPost("sign-in")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetailsDto))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetailsDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetailsDto))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetailsDto))]
    [SwaggerOperation(
        Description = "",
        OperationId = "SignInUser",
        Tags = ["User"]
    )]
    [SwaggerRequestExample(typeof(SignInUserRequest), typeof(SignInUserRequestExample))]
    public async Task<IActionResult> Handle([FromBody] SignInUserRequest request, CancellationToken cancellationToken = default)
    {
        await mediator.Send(request, cancellationToken);

        return NoContent();
    }
}

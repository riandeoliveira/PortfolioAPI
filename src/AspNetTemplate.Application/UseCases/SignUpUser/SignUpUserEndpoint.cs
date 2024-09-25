using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using AspNetTemplate.Application.Endpoints;

using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using AspNetTemplate.Domain.Dtos;

namespace AspNetTemplate.Application.UseCases.SignUpUser;

public sealed class SignUpUserEndpoint(IMediator mediator) : UserEndpoint
{
    [HttpPost("sign-up")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetailsDto))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ProblemDetailsDto))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetailsDto))]
    [SwaggerOperation(
        Description = "",
        OperationId = "SignUpUser",
        Tags = ["User"]
    )]
    [SwaggerRequestExample(typeof(SignUpUserRequest), typeof(SignUpUserRequestExample))]
    public async Task<IActionResult> Handle([FromBody] SignUpUserRequest request, CancellationToken cancellationToken = default)
    {
        await mediator.Send(request, cancellationToken);

        return StatusCode(StatusCodes.Status201Created);
    }
}

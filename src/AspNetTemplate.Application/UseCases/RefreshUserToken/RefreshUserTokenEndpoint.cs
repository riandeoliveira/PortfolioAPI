using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using AspNetTemplate.Application.Endpoints;

using Swashbuckle.AspNetCore.Annotations;

namespace AspNetTemplate.Application.UseCases.RefreshUserToken;

public sealed class RefreshUserTokenEndpoint(IMediator mediator) : UserEndpoint
{
    [Authorize]
    [HttpPost("refresh-token")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RefreshUserTokenResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    [SwaggerOperation(
        Description = "",
        OperationId = "RefreshUserToken",
        Tags = ["User"]
    )]
    public async Task<IActionResult> Handle([FromBody] RefreshUserTokenRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            RefreshUserTokenResponse response = await mediator.Send(request, cancellationToken);

            return Ok(response);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}

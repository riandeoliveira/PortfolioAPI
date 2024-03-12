using MediatR;

using Microsoft.AspNetCore.Mvc;


namespace Portfolio.Users.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/user")]
public sealed partial class UserController(IMediator mediator) : ControllerBase
{
    // NOTE: RemoveAsync -> excluir conta
    // NOTE: SignInAsync -> iniciar sessão
    // NOTE: SignOutAsync -> encerrar sessão
    // NOTE: SignUpAsync -> cadastrar usuário
}

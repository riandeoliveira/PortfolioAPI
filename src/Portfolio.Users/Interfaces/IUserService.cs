using Portfolio.Users.Requests;
using Portfolio.Users.Responses;
using Portfolio.Utils.Interfaces;

namespace Portfolio.Users.Interfaces;

public interface IUserService : IBaseService
{
    Task<TokenResponse> LoginAsync(LoginUserRequest request);

    Task<TokenResponse> SignInAsync(SignInUserRequest request);
}

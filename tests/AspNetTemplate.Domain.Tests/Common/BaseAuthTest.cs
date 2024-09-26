using Microsoft.AspNetCore.Http;

using Moq;

using AspNetTemplate.Domain.Tests.Extensions;
using AspNetTemplate.Domain.Tests.Factories;
using AspNetTemplate.Infrastructure.Services;
using AspNetTemplate.Infrastructure.Extensions;
using AspNetTemplate.Application.UseCases.SignUpUser;

namespace AspNetTemplate.Domain.Tests.Common;

public abstract class BaseAuthTest(AspNetTemplateWebApplicationFactory factory) : BaseTest(factory)
{
    private static readonly DefaultHttpContext HttpContext = new();

    private static AuthService AuthServiceMock
    {
        get
        {
            Mock<IHttpContextAccessor> httpContextAccessorMock = new();

            httpContextAccessorMock.Setup(accessor => accessor.HttpContext).Returns(HttpContext);

            AuthService authService = new(
                httpContextAccessorMock.Object
            );

            return authService;
        }
    }

    protected async Task AuthenticateAsync()
    {
        string email = _faker.Internet.Email();
        string password = _faker.Internet.StrongPassword();

        SignUpUserRequest request = new(email, password);

        await _client.SendPostAsync("/api/user/sign-up", request);
    }
}

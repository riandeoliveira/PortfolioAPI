using System.Net;

using FluentAssertions;

using AspNetTemplate.Application.UseCases.SignInUser;
using AspNetTemplate.Domain.Messages;
using AspNetTemplate.Domain.Tests.Common;
using AspNetTemplate.Domain.Tests.Extensions;
using AspNetTemplate.Domain.Tests.Factories;
using AspNetTemplate.Domain.Tests.Fixtures;
using AspNetTemplate.Infrastructure.Extensions;

namespace AspNetTemplate.Application.Tests.UseCases.SignInUser;

public sealed class SignInUserBusinessTest(AspNetTemplateWebApplicationFactory factory) : BaseAuthTest(factory)
{
    [Fact]
    public async Task Should_SignInUser()
    {
        SignInUserRequest signInRequest = new(DatabaseFixture.User_1.Email, DatabaseFixture.User_1.Password);

        HttpResponseMessage response = await _client.SendPostAsync("/api/user/sign-in", signInRequest);

        response.Should().HaveStatusCode(HttpStatusCode.OK);
    }

    [Fact]
    public async Task ShouldNot_SignInUser_WithUnregisteredEmail()
    {
        SignInUserRequest request = new(
            _faker.Internet.Email(),
            _faker.Internet.StrongPassword()
        );

        HttpResponseMessage response = await _client.SendPostAsync("/api/user/sign-in", request);

        string responseMessage = await response.Content.ReadAsStringAsync();
        string message = responseMessage.Trim('"');
        string expectedMessage = Messages_PT_BR.EmailIsNotRegistered;

        response.Should().HaveStatusCode(HttpStatusCode.BadRequest);

        message.Should().Be(expectedMessage);
    }
}

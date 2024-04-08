using System.Net;

using FluentAssertions;

using Portfolio.Application.UseCases.SignUpUser;
using Portfolio.Domain.Dtos;
using Portfolio.Domain.Messages;
using Portfolio.Domain.Tests.Common;
using Portfolio.Domain.Tests.Extensions;
using Portfolio.Domain.Tests.Factories;
using Portfolio.Domain.Tests.Fixtures;
using Portfolio.Infrastructure.Extensions;

namespace Portfolio.Application.Tests.UseCases.SignUpUser;

public sealed class SignUpUserBusinessTest(PortfolioWebApplicationFactory factory) : BaseAuthTest(factory)
{
    [Fact]
    public async Task Should_SignUpUser()
    {
        SignUpUserRequest request = new(
            _faker.Internet.Email(),
            _faker.Internet.StrongPassword()
        );

        HttpResponseMessage response = await _client.SendPostAsync("/api/user/sign-up", request);

        TokenDto body = await response.GetBodyAsync<TokenDto>();

        bool userExists = await _userRepository.ExistAsync(body.UserId);

        response.Should().HaveStatusCode(HttpStatusCode.OK);

        userExists.Should().BeTrue();
    }

    [Fact]
    public async Task ShouldNot_SignUpUser_WithAlreadyRegisteredEmail()
    {
        SignUpUserRequest request = new(
            DatabaseFixture.User_2.Email,
            _faker.Internet.StrongPassword()
        );

        HttpResponseMessage response = await _client.SendPostAsync("/api/user/sign-up", request);

        string responseMessage = await response.Content.ReadAsStringAsync();
        string message = responseMessage.Trim('"');
        string expectedMessage = Messages_PT_BR.EmailAlreadyExists;

        bool userAlreadyExists = await _userRepository.ExistAsync(DatabaseFixture.User_2.Id);

        response.Should().HaveStatusCode(HttpStatusCode.BadRequest);

        message.Should().Be(expectedMessage);

        userAlreadyExists.Should().BeTrue();
    }
}

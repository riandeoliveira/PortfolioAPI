using System.Net;
using System.Net.Http.Json;

using FluentAssertions;

using Portfolio.Application.UseCases.SignUpUser;
using Portfolio.Domain.Dtos;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Tests.Common;
using Portfolio.Domain.Tests.Extensions;
using Portfolio.Domain.Tests.Factories;

using Portolio.Infrastructure.Extensions;

namespace Portfolio.Application.Tests.UseCases.SignUpUser;

public sealed class SignUpUserBusinessTest(PortfolioWebApplicationFactory factory) : BaseTest(factory)
{
    [Fact]
    public async Task ShouldCreateAnUser()
    {
        SignUpUserRequest request = new(
            _faker.Internet.Email(),
            _faker.Internet.StrongPassword()
        );

        HttpResponseMessage response = await _client.PostAsJsonAsync("/api/user/sign-up", request);

        TokenDto body = await response.GetBody<TokenDto>();

        bool userExists = await _userRepository.ExistAsync(body.UserId);

        response.Should().HaveStatusCode(HttpStatusCode.OK);

        userExists.Should().BeTrue();
    }

    [Fact]
    public async Task ShouldNotUseAnAlreadyRegisteredEmail()
    {
        string email = _faker.Internet.Email();
        string password = _faker.Internet.Password();
        string expectedMessage = "Este 'e-mail' já está sendo usado.";

        User user = new()
        {
            Email = email,
            Password = password
        };

        await _userRepository.CreateAsync(user);
        await _unitOfWork.CommitAsync();

        SignUpUserRequest request = new(email, password);
        HttpResponseMessage response = await _client.PostAsJsonAsync("/api/user/sign-up", request);

        string responseMessage = await response.Content.ReadAsStringAsync();
        string message = responseMessage.Trim('"');

        bool userAlreadyExists = await _userRepository.ExistAsync(user.Id);

        response.Should().HaveStatusCode(HttpStatusCode.BadRequest);

        message.Should().Be(expectedMessage);

        userAlreadyExists.Should().BeTrue();
    }
}

using System.Net;
using System.Net.Http.Json;

using FluentAssertions;

using Portfolio.Application.UseCases.SignUpUser;
using Portfolio.Domain.Dtos;
using Portfolio.Domain.Entities;
using Portfolio.Infrastructure.Tools;
using Portfolio.WebApi.IntegrationTests.Common;
using Portfolio.WebApi.IntegrationTests.Extensions;
using Portfolio.WebApi.IntegrationTests.Factories;

using Portolio.Infrastructure.Extensions;

namespace Portfolio.WebApi.IntegrationTests.UseCaseTests.SignUpUser;

public sealed class SignUpUserBusinessTest(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
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

        User? createdUser = await _userRepository.FindOneOrThrowAsync(body.UserId);

        long now = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();
        bool isValidPassword = PasswordTool.Verify(request.Password, createdUser.Password);

        response.Should().HaveStatusCode(HttpStatusCode.OK);

        body.AccessToken.Should().NotBeNullOrEmpty();
        body.RefreshToken.Should().NotBeNullOrEmpty();
        body.Expires.Should().BeGreaterThan(now);
        body.UserId.Should().NotBe(Guid.Empty);

        ExecuteEntityTests(createdUser);

        createdUser.Email.Should().Be(request.Email);

        isValidPassword.Should().BeTrue();
    }

    [Fact]
    public async Task ShouldNotUseAnEmailAlreadyRegistered()
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

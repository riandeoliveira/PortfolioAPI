using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

using FluentAssertions;

using Portfolio.Application.UseCases.SignUpUser;
using Portfolio.Domain.Dtos;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Tests.Common;
using Portfolio.Domain.Tests.Extensions;
using Portfolio.Domain.Tests.Factories;
using Portfolio.Infrastructure.Tools;

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

        _client.DefaultRequestHeaders.AcceptLanguage.ParseAdd("pt-BR");

        HttpRequestMessage httpRequest = new(HttpMethod.Post, "/api/user/sign-up")
        {
            Content = new StringContent(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                "application/json"
            )
        };

        httpRequest.Headers.Add("Accept-Language", "pt-BR");

        HttpResponseMessage response = await _client.SendAsync(httpRequest);
        TokenDto body = await response.GetBody<TokenDto>();

        User? createdUser = await _userRepository.FindOneOrThrowAsync(body.UserId);

        bool isValidPassword = PasswordTool.Verify(request.Password, createdUser.Password);

        response.Should().HaveStatusCode(HttpStatusCode.OK);

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

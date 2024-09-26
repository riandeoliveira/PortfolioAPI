using System.Net;

using AspNetTemplate.Application.UseCases.SignUpUser;
using AspNetTemplate.Domain.Dtos;
using AspNetTemplate.Domain.Entities;
using AspNetTemplate.Domain.Messages;
using AspNetTemplate.Domain.Tests.Common;
using AspNetTemplate.Domain.Tests.Extensions;
using AspNetTemplate.Domain.Tests.Factories;
using AspNetTemplate.Infrastructure.Extensions;

using FluentAssertions;

using Microsoft.AspNetCore.WebUtilities;

namespace AspNetTemplate.Application.Tests.UseCases;

public sealed class SignUpUserTest(AspNetTemplateWebApplicationFactory factory) : BaseAuthTest(factory)
{
    private readonly string _requestUri = "/api/user/sign-up";

    [Fact]
    public async Task Should_SignUpUser()
    {
        SignUpUserRequest request = new(
            _faker.Internet.Email(),
            _faker.Internet.StrongPassword()
        );

        HttpResponseMessage response = await _client.SendPostAsync("/api/user/sign-up", request);

        IEnumerable<string> cookies = response.GetCookies();

        User? user = await _userRepository.FindOneAsync(x => x.Email == request.Email);

        response.Should().HaveStatusCode(HttpStatusCode.Created);

        ShouldHaveValidJwtCookies(cookies);

        user.Should().NotBeNull();
        user?.Email.Should().Be(request.Email);
    }

    [Fact]
    public async Task ShouldNot_SignUpUser_WithAlreadyRegisteredEmail()
    {
        (string email, string password) = await AuthenticateAsync();

        SignUpUserRequest request = new(email, password);

        HttpResponseMessage response = await _client.SendPostAsync(_requestUri, request);
        ProblemDetailsDto body = await response.GetBodyAsync<ProblemDetailsDto>();

        HttpStatusCode statusCode = HttpStatusCode.Conflict;
        int status = (int) statusCode;

        response.Should().HaveStatusCode(statusCode);

        body.Type.Should().Be($"https://httpstatuses.com/{status}");
        body.Title.Should().Be(Messages_PT_BR.EmailAlreadyExists);
        body.Status.Should().Be(status);
        body.Detail.Should().Be(ReasonPhrases.GetReasonPhrase(status));
        body.Instance.Should().Be(_requestUri);
    }
}

using System.Net;
using System.Text.RegularExpressions;

using AspNetTemplate.Application.UseCases.SignUpUser;
using AspNetTemplate.Domain.Tests.Common;
using AspNetTemplate.Domain.Tests.Extensions;
using AspNetTemplate.Domain.Tests.Factories;
using AspNetTemplate.Infrastructure.Extensions;
using AspNetTemplate.Application.Tests.Extensions;

using FluentAssertions;

namespace AspNetTemplate.Application.Tests.UseCases;

public sealed class SignUpUserTest(AspNetTemplateWebApplicationFactory factory) : BaseAuthTest(factory)
{
    private void GetJwtTokenFromCookies(string cookie)
    {
        string pattern = @"access_token=([^;]+)";

        Match? match = Regex.Match(cookie, pattern);

        if (match.Success)
        {
            string accessToken = match.Groups[1].Value;
            Console.WriteLine("Access Token: " + accessToken);
        }
        else
        {
            Console.WriteLine("Token not found.");
        }
    }

    [Fact]
    public async Task Should_SignUpUser()
    {
        SignUpUserRequest request = new(
            _faker.Internet.Email(),
            _faker.Internet.StrongPassword()
        );

        HttpResponseMessage response = await _client.SendPostAsync("/api/user/sign-up", request);

        response.Should().HaveStatusCode(HttpStatusCode.Created);
        response.ShouldHaveJwtCookies();

        // TODO: Verificar se o usuário foi criado no banco
        // TODO: Verificar se os cookies foram criados corretamente
        // TODO: Verificar se os cookies são jwt válidos
    }
}

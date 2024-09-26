using System.Text.RegularExpressions;

using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace AspNetTemplate.Application.Tests.Extensions;

public static class HttpResponseMessageAssertionsExtensions
{
    public static HttpResponseMessageAssertions ShouldHaveJwtCookies(this HttpResponseMessage response)
    {
        return new HttpResponseMessageAssertions(response);
    }
}

public class HttpResponseMessageAssertions(HttpResponseMessage subject) : ReferenceTypeAssertions<HttpResponseMessage, HttpResponseMessageAssertions>(subject)
{
    protected override string Identifier => "HttpResponseMessage";

    public AndConstraint<HttpResponseMessageAssertions> HaveJwtCookies(string cookieName = "access_token")
    {
        IEnumerable<string> cookies = Subject.Headers.GetValues("Set-Cookie");

        bool hasJwt = false;

        string pattern = $@"{cookieName}=([^;]+)";

        foreach (string cookie in cookies)
        {
            if (Regex.IsMatch(cookie, pattern))
            {
                hasJwt = true;

                break;
            }
        }

        Execute.Assertion
            .ForCondition(hasJwt)
            .FailWith($"Expected {Identifier} to have a JWT cookie named '{cookieName}', but it did not.");

        return new AndConstraint<HttpResponseMessageAssertions>(this);
    }
}

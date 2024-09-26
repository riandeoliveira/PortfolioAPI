// using System.Net;

// using FluentAssertions;

// using AspNetTemplate.Application.UseCases.UpdateUser;
// using AspNetTemplate.Domain.Dtos;
// using AspNetTemplate.Domain.Entities;
// using AspNetTemplate.Domain.Messages;
// using AspNetTemplate.Domain.Tests.Common;
// using AspNetTemplate.Domain.Tests.Extensions;
// using AspNetTemplate.Domain.Tests.Factories;
// using AspNetTemplate.Domain.Tests.Fixtures;
// using AspNetTemplate.Infrastructure.Extensions;

// namespace AspNetTemplate.Application.Tests.UseCases.UpdateUser;

// public sealed class UpdateUserBusinessTest(AspNetTemplateWebApplicationFactory factory) : BaseAuthTest(factory)
// {
//     [Fact]
//     public async Task Should_UpdateUser()
//     {
//         await AuthenticateAsync();

//         UpdateUserRequest request = new(
//             _faker.Internet.Email(),
//             _faker.Internet.StrongPassword()
//         );

//         HttpResponseMessage response = await _client.SendPutAsync("/api/user", request);

//         UserDto currentUser = await GetCurrentUserAsync();

//         User user = await _userRepository.FindOneOrThrowAsync(currentUser.Id);

//         response.Should().HaveStatusCode(HttpStatusCode.NoContent);

//         user.Email.Should().NotBeNullOrWhiteSpace();
//         user.Email.Should().Be(request.Email);

//         user.Password.Should().NotBeNullOrWhiteSpace();
//         user.Password.Should().Be(request.Password);
//     }

//     [Fact]
//     public async Task Should_UpdateUser_WithoutChanges()
//     {
//         await AuthenticateAsync();

//         UpdateUserRequest request = new(
//             DatabaseFixture.User_1.Email,
//             DatabaseFixture.User_1.Password
//         );

//         HttpResponseMessage response = await _client.SendPutAsync("/api/user", request);

//         UserDto currentUser = await GetCurrentUserAsync();

//         User user = await _userRepository.FindOneOrThrowAsync(currentUser.Id);

//         response.Should().HaveStatusCode(HttpStatusCode.NoContent);

//         user.Email.Should().NotBeNullOrWhiteSpace();
//         user.Email.Should().Be(request.Email);

//         user.Password.Should().NotBeNullOrWhiteSpace();
//         user.Password.Should().Be(request.Password);
//     }

//     [Fact]
//     public async Task ShouldNot_UpdateUser_WithoutAuthentication()
//     {
//         UpdateUserRequest request = new(
//             DatabaseFixture.User_1.Email,
//             DatabaseFixture.User_1.Password
//         );

//         HttpResponseMessage response = await _client.SendPutAsync("/api/user", request);

//         response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
//         response.ReasonPhrase.Should().Be("Unauthorized");
//     }

//     [Fact]
//     public async Task ShouldNot_UpdateUser_WithAlreadyRegisteredEmail()
//     {
//         await AuthenticateAsync();

//         UpdateUserRequest request = new(
//             DatabaseFixture.User_2.Email,
//             _faker.Internet.StrongPassword()
//         );

//         HttpResponseMessage response = await _client.SendPutAsync("/api/user", request);

//         string responseMessage = await response.Content.ReadAsStringAsync();
//         string message = responseMessage.Trim('"');
//         string expectedMessage = Messages_PT_BR.EmailAlreadyExists;

//         bool userAlreadyExists = await _userRepository.ExistAsync(DatabaseFixture.User_2.Id);

//         response.Should().HaveStatusCode(HttpStatusCode.BadRequest);

//         message.Should().Be(expectedMessage);

//         userAlreadyExists.Should().BeTrue();
//     }
// }

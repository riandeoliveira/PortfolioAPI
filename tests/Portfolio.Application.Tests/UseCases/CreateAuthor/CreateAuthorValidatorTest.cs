using Portfolio.Application.UseCases.CreateAuthor;
using Portfolio.Domain.Tests.Common;
using Portfolio.Domain.Tests.Factories;

namespace Portfolio.Application.Tests.UseCases.CreateAuthor;

public sealed class CreateAuthorValidatorTest(PortfolioWebApplicationFactory factory) : BaseValidationTest(factory)
{
    public CreateAuthorRequest CreateRequest(
        string? name = null,
        string? fullName = null,
        string? position = null,
        string? description = null,
        string? avatarUrl = null,
        string? spotifyAccountName = null
    ) => new(
            Name: name ?? _faker.Name.FirstName(),
            FullName: fullName ?? _faker.Name.FullName(),
            Position: position ?? _faker.Name.JobTitle(),
            Description: description ?? _faker.Lorem.Sentence(),
            AvatarUrl: avatarUrl ?? _faker.Internet.Url(),
            SpotifyAccountName: spotifyAccountName ?? _faker.Internet.UserName()
        );

    [InlineData(EMPTY_STRING, "O 'nome' deve ser informado.")]
    [InlineData(STRING_WITH_SIZE_65, "O 'nome' deve possuir no máximo 64 caracteres.")]
    [Theory]
    public async Task Name_ValidationTest(string name, string expectedMessage) =>
        await ExecuteAsync("/api/author", CreateRequest(name: name), expectedMessage);

    [InlineData(EMPTY_STRING, "O 'nome completo' deve ser informado.")]
    [InlineData(STRING_WITH_SIZE_129, "O 'nome completo' deve possuir no máximo 128 caracteres.")]
    [Theory]
    public async Task FullName_ValidationTest(string fullName, string expectedMessage) =>
        await ExecuteAsync("/api/author", CreateRequest(fullName: fullName), expectedMessage);

    [InlineData(EMPTY_STRING, "O 'cargo' deve ser informado.")]
    [InlineData(STRING_WITH_SIZE_65, "O 'cargo' deve possuir no máximo 64 caracteres.")]
    [Theory]
    public async Task Position_ValidationTest(string position, string expectedMessage) =>
        await ExecuteAsync("/api/author", CreateRequest(position: position), expectedMessage);

    [InlineData(EMPTY_STRING, "A 'descrição' deve ser informada.")]
    [InlineData(STRING_WITH_SIZE_1025, "A 'descrição' deve possuir no máximo 1024 caracteres.")]
    [Theory]
    public async Task Description_ValidationTest(string description, string expectedMessage) =>
        await ExecuteAsync("/api/author", CreateRequest(description: description), expectedMessage);

    [InlineData(EMPTY_STRING, "A 'url do avatar' deve ser informada.")]
    [InlineData(STRING_WITH_SIZE_513, "A 'url do avatar' deve possuir no máximo 512 caracteres.")]
    [Theory]
    public async Task AvatarUrl_ValidationTest(string avatarUrl, string expectedMessage) =>
        await ExecuteAsync("/api/author", CreateRequest(avatarUrl: avatarUrl), expectedMessage);

    [InlineData(STRING_WITH_SIZE_65, "O 'nome da conta do Spotify' deve possuir no máximo 64 caracteres.")]
    [Theory]
    public async Task SpotifyAccountName_ValidationTest(string spotifyAccountName, string expectedMessage) =>
        await ExecuteAsync("/api/author", CreateRequest(spotifyAccountName: spotifyAccountName), expectedMessage);
}

using Microsoft.AspNetCore.WebUtilities;

using System.Text.Json.Serialization;

namespace AspNetTemplate.Infra.Data.Dtos;

public sealed record ProblemDetailsDto(string Title, int Status, string Instance)
{
    [JsonPropertyOrder(-3)]
    public string Type => $"https://httpstatuses.com/{Status}";

    [JsonPropertyOrder(-2)]
    public string Title { get; init; } = Title;

    [JsonPropertyOrder(-1)]
    public int Status { get; init; } = Status;

    [JsonPropertyOrder(0)]
    public string Detail => ReasonPhrases.GetReasonPhrase(Status);

    [JsonPropertyOrder(1)]
    public string Instance { get; init; } = Instance;
}

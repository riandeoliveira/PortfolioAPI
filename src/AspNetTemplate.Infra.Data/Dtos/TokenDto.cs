namespace AspNetTemplate.Infra.Data.Dtos;

public sealed record TokenDto(string Value, DateTime ExpiresIn);

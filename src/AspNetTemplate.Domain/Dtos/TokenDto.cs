namespace AspNetTemplate.Domain.Dtos;

public sealed record TokenDto(string Value, DateTime ExpiresIn);

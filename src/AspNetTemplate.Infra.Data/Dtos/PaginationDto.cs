using AspNetTemplate.Domain.Entities;

namespace AspNetTemplate.Infra.Data.Dtos;

public sealed record PaginationDto<TEntity>(
    int PageNumber,
    int PageSize,
    int TotalPages,
    IEnumerable<TEntity> Entities
) where TEntity : BaseEntity;

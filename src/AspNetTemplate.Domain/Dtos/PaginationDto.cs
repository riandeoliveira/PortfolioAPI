using AspNetTemplate.Domain.Entities;

namespace AspNetTemplate.Domain.Dtos;

public record PaginationDto<TEntity>(
    int PageNumber,
    int PageSize,
    int TotalPages,
    IEnumerable<TEntity> Entities
) where TEntity : BaseEntity;

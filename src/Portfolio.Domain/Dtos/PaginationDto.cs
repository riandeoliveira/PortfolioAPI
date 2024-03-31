using Portfolio.Domain.Entities;

namespace Portfolio.Domain.Dtos;

public record PaginationDto<TEntity>(
    int PageNumber,
    int PageSize,
    int TotalPages,
    IEnumerable<TEntity> Entities
) where TEntity : BaseEntity;

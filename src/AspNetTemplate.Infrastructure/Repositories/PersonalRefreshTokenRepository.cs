using AspNetTemplate.Domain.Entities;
using AspNetTemplate.Domain.Interfaces;
using AspNetTemplate.Infrastructure.Contexts;
using AspNetTemplate.Infrastructure.Repositories.Base;

namespace AspNetTemplate.Infrastructure.Repositories;

public sealed class PersonalRefreshTokenRepository(
    ApplicationDbContext context
) : BaseRepository<PersonalRefreshToken>(context), IPersonalRefreshTokenRepository
{
}

using AspNetTemplate.Domain.Entities;
using AspNetTemplate.Infra.Data.Contexts;
using AspNetTemplate.Infra.Data.Interfaces;

namespace AspNetTemplate.Infra.Data.Repositories;

public sealed class PersonalRefreshTokenRepository(
    ApplicationDbContext context
) : BaseRepository<PersonalRefreshToken>(context), IPersonalRefreshTokenRepository
{
}

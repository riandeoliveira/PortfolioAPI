using AspNetTemplate.Contexts;
using AspNetTemplate.Entities;
using AspNetTemplate.Interfaces;

namespace AspNetTemplate.Repositories;

public class PersonalRefreshTokenRepository(AppDbContext context)
    : BaseRepository<PersonalRefreshToken>(context),
        IPersonalRefreshTokenRepository { }

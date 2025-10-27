using AspNetTemplate.Contexts;
using AspNetTemplate.Entities;
using AspNetTemplate.Interfaces;

namespace AspNetTemplate.Repositories;

public class PasswordResetTokenRepository(AppDbContext context)
    : BaseRepository<PasswordResetToken>(context),
        IPasswordResetTokenRepository { }

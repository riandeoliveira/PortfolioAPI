using AspNetTemplate.Contexts;
using AspNetTemplate.Entities;
using AspNetTemplate.Interfaces;

namespace AspNetTemplate.Repositories;

public class UserRepository(AppDbContext context)
    : BaseRepository<User>(context),
        IUserRepository { }

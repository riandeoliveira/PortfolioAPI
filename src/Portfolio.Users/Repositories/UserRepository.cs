using Portfolio.Entities;
using Portfolio.Entities.Context;
using Portfolio.Users.Interfaces;
using Portfolio.Utils.Repositories;

namespace Portfolio.Users.Repositories;

public class UserRepository(ApplicationDbContext context) : BaseRepository<User>(context), IUserRepository
{

}

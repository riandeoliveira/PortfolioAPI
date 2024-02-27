using Portfolio.Domain.Entities;
using Portfolio.Domain.Context;
using Portfolio.Users.Interfaces;
using Portfolio.Utils.Repositories;

namespace Portfolio.Users.Repositories;

public class UserRepository(DatabaseContext context) : BaseRepository<User>(context), IUserRepository
{

}

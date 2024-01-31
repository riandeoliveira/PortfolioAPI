using Microsoft.AspNetCore.Mvc;

using PortfolioAPI.Context;
using PortfolioAPI.Entities;

namespace PortfolioAPI.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController(ApplicationDbContext context) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;

        [HttpGet]
        public ActionResult<IList<User>> GetAsync()
        {
            var users = _context.Users.ToList();

            return Ok(users);
        }
    }
}

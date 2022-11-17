using Microsoft.EntityFrameworkCore;
using user_service.Model;

namespace user_service.Context
{
    public class UserContext : DbContext
    {
        public DbSet<User>? Users { get; set; }

        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }
    }
}

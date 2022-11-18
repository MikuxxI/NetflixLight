using user_service.Models;
using Microsoft.EntityFrameworkCore;

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

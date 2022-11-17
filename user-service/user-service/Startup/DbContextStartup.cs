using Microsoft.EntityFrameworkCore;
using user_service.Context;
namespace user_service.Startup
{
    public static class DbContextStartup
    {
        public static IServiceCollection UseDbContextConfiguration(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<UserContext>(options =>
                options.UseSqlServer(connectionString)
            );

            return services;
        }
    }
}

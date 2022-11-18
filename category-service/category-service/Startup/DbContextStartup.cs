using category_service.Context;
using Microsoft.EntityFrameworkCore;
namespace category_service.Startup
{
    public static class DbContextStartup
    {
        public static IServiceCollection UseDbContextConfiguration(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<CategorieContext>(options =>
                options.UseSqlServer(connectionString)
            );

            return services;
        }
    }
}

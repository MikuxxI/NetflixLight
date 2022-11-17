namespace Payment_service.Startup;

public static class DbContextStartup
{
    public static IServiceCollection UseDbContextConfiguration(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<PaymentContext>(options =>
            options.UseSqlServer(connectionString)
        );
    }
}
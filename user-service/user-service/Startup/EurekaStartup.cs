using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Eureka;

namespace user_service.Startup
{
    public static class EurekaStartup
    {
        public static IServiceCollection UseEurekaConfiguration(this IServiceCollection services)
        {
            services.AddServiceDiscovery(options => options.UseEureka());

            return services;
        }
    }
}

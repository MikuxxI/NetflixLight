using category_service.Messaging;
using Steeltoe.CloudFoundry.Connector.RabbitMQ;
using Steeltoe.Messaging.RabbitMQ.Config;
using Steeltoe.Messaging.RabbitMQ.Extensions;

namespace category_service.Startup
{
    public static class RabbitMqStartup
    {
        public static IServiceCollection UseRabbitConfiguration(this IServiceCollection services, ConfigurationManager configManager)
        {
            services.AddRabbitMQConnection(configManager);
            services.AddRabbitServices(true);
            services.AddRabbitAdmin();
            services.AddRabbitTemplate();

            services.AddRabbitExchange("ms.categorie", ExchangeType.TOPIC);

            services.AddRabbitQueue("ms.categorie.deletion");

            // Configuration du service qui captera les évènements reçus
            services.AddSingleton<CategorieEventHandler>();
            services.AddRabbitListeners<CategorieEventHandler>();

            return services;
        }
    }
}

using Steeltoe.Connector.RabbitMQ;
using Steeltoe.Messaging.RabbitMQ.Config;
using Steeltoe.Messaging.RabbitMQ.Extensions;
using user_service.Messaging;

namespace user_service.Startup
{
    public static class RabbitMqStartup
    {
        public static IServiceCollection UseRabbitConfiguration(this IServiceCollection services, ConfigurationManager configManager)
        {
            // Configuration générale
            services.AddRabbitMQConnection(configManager);
            services.AddRabbitServices(true);
            services.AddRabbitAdmin();
            services.AddRabbitTemplate();

            services.AddRabbitQueue("ms.user.deletion");

            services.AddRabbitExchange("ms.user", ExchangeType.TOPIC);


            // Liaison de cette queue à l'Exchange ms.produit
            services.AddRabbitBinding("userDeletion", Binding.DestinationType.QUEUE, (p, b) => {
                var binding = b as QueueBinding;

                binding.Exchange = "ms.user";
                binding.Destination = "ms.user.deletion";
                binding.RoutingKey = "user.deletion.askfor"; // On écoute les demandes de suppression
            });

           

            // Configuration du service qui captera les évènements reçus
            services.AddSingleton<UserEventHandler>();
            services.AddRabbitListeners<UserEventHandler>();

            return services;
        }
    }
}

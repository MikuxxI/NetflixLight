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

            // Configuration de la queue "produit en suppression" pour le service commentaire
            services.AddRabbitQueue("ms.user.deletion.user");

            // Liaison de cette queue à l'Exchange ms.produit
            services.AddRabbitBinding("userDeletion", Binding.DestinationType.QUEUE, (p, b) => {
                var binding = b as QueueBinding;

                binding.Exchange = "ms.user";
                binding.Destination = "ms.user.deletion.user";
                binding.RoutingKey = "user.deletion.askfor"; // On écoute les demandes de suppression
            });

            // Configuration de la queue "produit détaillé" pour le service commentaire
            services.AddRabbitQueue("ms.user.detailed.user");

            // Liaison de cette queue à l'Exchange ms.produit
            services.AddRabbitBinding("userDetailed", Binding.DestinationType.QUEUE, (p, b) => {
                var binding = b as QueueBinding;

                binding.Exchange = "ms.user";
                binding.Destination = "ms.user.detailed.user";
                binding.RoutingKey = "user.detailed";
            });

            // Configuration du service qui captera les évènements reçus
            services.AddSingleton<UserEventHandler>();
            services.AddRabbitListeners<UserEventHandler>();

            return services;
        }
    }
}

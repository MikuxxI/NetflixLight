using category_service.Messaging;
using Steeltoe.Connector.RabbitMQ;
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

            // Configuration de la queue "produit en suppression" pour le service commentaire
            services.AddRabbitQueue("ms.categorie.deletion");

            services.AddRabbitExchange("ms.categorie", ExchangeType.TOPIC);

            // Liaison de cette queue à l'Exchange ms.produit
            services.AddRabbitBinding("categorieDeletion", Binding.DestinationType.QUEUE, (p, b) => {
                var binding = b as QueueBinding;

                binding.Exchange = "ms.categorie";
                binding.Destination = "ms.categorie.deletion";
                binding.RoutingKey = "categorie.deletion.askfor"; // On écoute les demandes de suppression
            });

            // Configuration du service qui captera les évènements reçus
            services.AddSingleton<CategorieEventHandler>();
            services.AddRabbitListeners<CategorieEventHandler>();

            return services;
        }
    }
}

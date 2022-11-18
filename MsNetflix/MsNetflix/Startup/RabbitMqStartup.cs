using Payment_service.Messaging;
using Steeltoe.Connector.RabbitMQ;
using Steeltoe.Messaging.RabbitMQ.Config;
using Steeltoe.Messaging.RabbitMQ.Extensions;

namespace Payment_service.Startup;

public static class RabbitMqStartup
{
    public static IServiceCollection UseRabbitConfiguration(this IServiceCollection services, ConfigurationManager configManager)
    {
        // Configuration générale
        services.AddRabbitMQConnection(configManager);
        services.AddRabbitServices(true);
        services.AddRabbitAdmin();
        services.AddRabbitTemplate();

        // Configuration de la queue "maj sold user" pour le service payment
        services.AddRabbitQueue("ms.user.update");

        // Liaison de cette queue à l'Exchange ms.user.update
        services.AddRabbitBinding("userUpdate", Binding.DestinationType.QUEUE, (p, b) => {
            var binding = b as QueueBinding;

            binding.Exchange = "ms.payment";
            binding.Destination = "ms.user.update";
            binding.RoutingKey = "user.update.askfor";
        });
        

        // Configuration du service qui captera les évènements reçus
        services.AddSingleton<UserEventHandler>();
        services.AddRabbitListeners<UserEventHandler>();

        return services;
    }
}


using Steeltoe.Messaging.RabbitMQ.Core;

namespace Payment_service.Messaging;

public class UserUpdateEvent
{
    public int UserId { get; set; }

    public double Sold { get; set; }
}


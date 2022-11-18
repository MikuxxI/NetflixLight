using Payment_service.Context;
using Payment_service.Model;
using Steeltoe.Messaging.RabbitMQ.Attributes;
using Steeltoe.Messaging.RabbitMQ.Core;
using Steeltoe.Messaging.RabbitMQ.Extensions;

namespace Payment_service.Messaging;
public class UserEventHandler
{
    private readonly IServiceProvider _services;

    public UserEventHandler(IServiceProvider services)
    {
        _services = services;
    }

    [RabbitListener(Binding = "userUpdate")]
    public void on(UserUpdateEvent evt)
    {
        RabbitTemplate rabbitTemplate = _services.GetRabbitTemplate();

        using (var scope = _services.CreateScope())
        {
            var paymentContext = scope.ServiceProvider.GetService<PaymentContext>();
            User user = paymentContext.Users.First(u => u.Id == evt.UserId);

            user.Sold += evt.Sold;

            paymentContext.SaveChanges();

            rabbitTemplate.ConvertAndSend("ms.payment", "user.update", new UserUpdateCommand
            {
                UserId = evt.UserId,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Username = user.Username,
                Password = user.Password,
                Sold = evt.Sold,
                AdminRole = user.AdminRole,
            });
        }
    }


}


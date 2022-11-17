using Steeltoe.Messaging.RabbitMQ.Attributes;
using Steeltoe.Messaging.RabbitMQ.Config;
using Steeltoe.Messaging.RabbitMQ.Core;
using Steeltoe.Messaging.RabbitMQ.Extensions;
using user_service.Context;
using user_service.Model;

namespace user_service.Messaging
{
    public class UserEventHandler
    {
        private readonly IServiceProvider _services;

        public UserEventHandler(IServiceProvider services)
        {
            _services = services;
        }

        [RabbitListener(Binding = "userDeletion")]
        public void on(UserDeletionEvent evt)
        {
            RabbitTemplate rabbitTemplate = _services.GetRabbitTemplate();

            using (var scope = _services.CreateScope())
            {
                var userContext = scope.ServiceProvider.GetService<UserContext>();
                int count = userContext.Users.Count(c => c.Id == evt.UserId);

                if (count == 0)
                {
                    rabbitTemplate.ConvertAndSend("ms.user", "user.deletion.ok", new UserDeleteOkUser
                    {
                        UserId = evt.UserId
                    });
                }

                else
                {
                    rabbitTemplate.ConvertAndSend("ms.user", "user.deletion.ko", new UserDeleteKoUser
                    {
                        UserId = evt.UserId
                    });
                }
            }
        }

        [RabbitListener(Binding = "userDetailed")]
        public void on(UserDetailedEvent evt)
        {
            RabbitTemplate rabbitTemplate = _services.GetRabbitTemplate();

            using (var scope = _services.CreateScope())
            {
                var userContext = scope.ServiceProvider.GetService<UserContext>();
                User user = userContext.Users.First(c => c.Id == evt.UserId);
                

               

                userContext.SaveChanges();
            }
        }
    }
}

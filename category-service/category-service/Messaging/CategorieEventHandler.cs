using category_service.Context;
using Steeltoe.Messaging.RabbitMQ.Attributes;
using Steeltoe.Messaging.RabbitMQ.Core;
using Steeltoe.Messaging.RabbitMQ.Extensions;

namespace category_service.Messaging
{
    public class CategorieEventHandler
    {
        private readonly IServiceProvider _services;

        public CategorieEventHandler(IServiceProvider services)
        {
            _services = services;
        }

        [RabbitListener(Binding = "categorieDeletion")]
        public void on(CategorieDeletionEvent evt)
        {
            RabbitTemplate rabbitTemplate = _services.GetRabbitTemplate();

            using (var scope = _services.CreateScope())
            {
                var categorieContext = scope.ServiceProvider.GetService<CategorieContext>();
                int count = categorieContext.Categories.Count(c => c.Id == evt.CategorieId);

                if (count == 0)
                {
                    rabbitTemplate.ConvertAndSend("ms.categorie", "categorie.deletion.ok", new CategorieDeleteOkCommand
                    {
                        CategorieId = evt.CategorieId
                    });
                }

                else
                {
                    rabbitTemplate.ConvertAndSend("ms.categorie", "categorie.deletion.ko", new CategorieDeleteKoCommand
                    {
                        CategorieId = evt.CategorieId
                    });
                }
            }
        }
    }
}

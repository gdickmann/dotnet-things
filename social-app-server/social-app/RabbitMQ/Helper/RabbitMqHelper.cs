using MassTransit;
using social_app.RabbitMQ.Services;

namespace social_app.RabbitMQ.Helper
{
    public class RabbitMqHelper
    {

        private readonly static string POSTS_QUEUE_NAME = "posts";

        public static async void StartRabbit()
        {
            var busControl = Bus.Factory.CreateUsingRabbitMq(rabbitConfigurator =>
            {
                CreateQueues(rabbitConfigurator);
            });

            try
            {
                await busControl.StartAsync();
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void CreateQueues(IRabbitMqBusFactoryConfigurator configurator)
        {
            configurator.ReceiveEndpoint(POSTS_QUEUE_NAME, e =>
            {
                e.Consumer<PostService>();
            });
        }
    }
}

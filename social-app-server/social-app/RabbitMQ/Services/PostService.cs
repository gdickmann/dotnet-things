using MassTransit;
using social_app.Models;
using social_app.Models.Request;

namespace social_app.RabbitMQ.Services
{
    /** TODO: rename it to PostConsumer */
    public class PostService : IConsumer<Post>
    {
        public async Task Consume(ConsumeContext<Post> context)
        {
            var data = context.Message;
            Console.WriteLine($"Consumed message: {data}");
        }
    }
    class PostServiceDefinition :
        ConsumerDefinition<PostService>
    {
        public PostServiceDefinition()
        {
            EndpointName = "posts";
            ConcurrentMessageLimit = 8;
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<PostService> consumerConfigurator)
        {
            endpointConfigurator.UseMessageRetry(r => r.Intervals(100, 200, 500, 800, 1000));
            endpointConfigurator.UseInMemoryOutbox();
        }
    }

}

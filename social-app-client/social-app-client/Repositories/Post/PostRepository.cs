using RabbitMQ.Client;
using social_app_client.Models.Post;
using System.Text;

namespace social_app_client.Repositories.Post
{
    public class PostRepository : IPostRepository
    {

        private readonly ILogger<PostRepository> _logger;

        public PostRepository(ILogger<PostRepository> logger)
        {
            _logger = logger;
        }

        public void InsertIntoQueue(PostRequest request)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "posts",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var body = Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(request));

                channel.BasicPublish(exchange: "",
                                     routingKey: "posts",
                                     basicProperties: null,
                                     body: body);

                _logger.LogInformation("Post sent via RabbitMQ", DateTime.UtcNow);
            }
        }
    }
}

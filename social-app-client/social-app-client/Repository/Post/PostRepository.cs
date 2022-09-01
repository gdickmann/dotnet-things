using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using social_app_client.Models.Post;
using System.Text;

namespace social_app_client.Repository.Post
{
    public class PostRepository : IPostRepository
    {
        public void InsertIntoQueue(PostRequest request)
        {
            /** This is the simplest RabbitMQ implementation and isn't recommended to be used in production.
             * However, my focus isn't improving RabbitMQ security for now.
             * To a more complete and secure implementation, read: https://www.rabbitmq.com/confirms.html
             */
            var factory = new ConnectionFactory { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "posts",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var message = Newtonsoft.Json.JsonConvert.SerializeObject(request);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "posts",
                                     basicProperties: null,
                                     body: body);

                Console.WriteLine(" [x] Sent {0}", message);
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using social_app_client.Models.Post;
using System.Text;

namespace social_app_client.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        
        private readonly ILogger<PostController> _logger;

        public PostController(ILogger<PostController> logger)
        {               
            _logger = logger;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(PostRequest request)
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

                string message = "Hello World!";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "posts",
                                     routingKey: "posts",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }

            return Ok();
        }
    }
}

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using social_app.Models.Request;
using social_app.Repositories;
using System.Text;

namespace social_app.RabbitMQ.Services
{
    public class PostService : BackgroundService
    {

        private readonly IServiceProvider _sp;
        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public PostService(IServiceProvider sp)
        {
            _sp = sp;

            _factory = new ConnectionFactory() { HostName = "localhost" };

            _connection = _factory.CreateConnection();

            _channel = _connection.CreateModel();

            _channel.QueueDeclare(
                queue: "users",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (stoppingToken.IsCancellationRequested)
            {
                _channel.Dispose();
                _connection.Dispose();
                return Task.CompletedTask;
            }

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();

                var message = Encoding.UTF8.GetString(body);

                Console.WriteLine(" [x] Received {0}", message);


                Task.Run(() =>
                {
                    var chunks = message.Split("|");

                    var hero = new PostRequest();
                    if (chunks.Length == 7)
                    {
                        hero.Title = chunks[1];
                    }

                    using (var scope = _sp.CreateScope())
                    {
                        var db = scope.ServiceProvider.GetRequiredService<IPostRepository>();
                        db.Create(hero);
                    }
                });
            };

            _channel.BasicConsume(queue: "users", autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }
    }
}

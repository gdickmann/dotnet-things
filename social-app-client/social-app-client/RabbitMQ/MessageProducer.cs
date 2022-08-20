using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace social_app_client.RabbitMQ
{
    public class MessageProducer : IMessageProducer
    {
        /** TODO: abstract */
        private readonly static string USERS_QUEUE = "users";

        public MessageProducer() {}

        public void Send<T>(T message)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };

            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(USERS_QUEUE);

            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "", routingKey: USERS_QUEUE, body: body);
        }

    }
}

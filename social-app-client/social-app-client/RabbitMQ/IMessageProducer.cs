namespace social_app_client.RabbitMQ
{
    public interface IMessageProducer
    {
        void Send<T>(T message);
    }
}

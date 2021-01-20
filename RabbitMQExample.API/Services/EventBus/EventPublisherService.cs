using RabbitMQ.Client;
using RabbitMQExample.API.Contracts.Services;
using System.Text;
using System.Text.Json;

namespace RabbitMQExample.API.EventBus
{
    public class EventPublisherService : IEventPublisherService
    {
        public void Publish(object message)
        {
            ConnectionFactory factory = new ConnectionFactory { HostName = "host.docker.internal", Port = 999 };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            var serializedMessage = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(serializedMessage);

            channel.BasicPublish(exchange: "ApiExchange", body: body, routingKey: "");
        }
    }
}
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace RabbitMQExample.API.EventBus
{
    public class EventBusPublisher
    {
        public void PublishEvent(object message)
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
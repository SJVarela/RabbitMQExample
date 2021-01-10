using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitMQExample.API.EventBus
{
    public class EventBusListener : IHostedService
    {
        private IModel _channel;

        private void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            Console.WriteLine(Encoding.UTF8.GetString(e.Body.ToArray()));
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            ConnectionFactory factory = new ConnectionFactory { HostName = "host.docker.internal", Port = 999 };
            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();
            _channel.ExchangeDeclare("ApiExchange", ExchangeType.Fanout);
            _channel.QueueDeclare(queue: "APIQueue");
            _channel.QueueBind("APIQueue", "ApiExchange", "");

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += Consumer_Received;
            _channel.BasicConsume("APIQueue", true, consumer);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _channel.Dispose();

            return Task.CompletedTask;
        }
    }
}
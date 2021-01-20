namespace RabbitMQExample.API.Contracts.Services
{
    public interface IEventPublisherService
    {
        void Publish(object message);
    }
}
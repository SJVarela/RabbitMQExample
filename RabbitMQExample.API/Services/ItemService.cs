using Mapster;
using RabbitMQExample.API.Contracts.Services;
using RabbitMQExample.API.Models;
using RabbitMQExample.DataAccess.Contracts;
using RabbitMQExample.DataAccess.Models;

namespace RabbitMQExample.API.Services
{
    public class ItemService : IItemService
    {
        private readonly IDbClient<Item> _dbClient;
        private readonly IEventPublisherService _eventPublisher;

        public ItemService(IDbClient<Item> dbClient, IEventPublisherService eventPublisher)
        {
            _dbClient = dbClient;
            _eventPublisher = eventPublisher;
        }

        public ItemDto SaveItem(ItemDto item)
        {
            var entity = item.Adapt<Item>();
            var result = _dbClient.Create(entity);
            _eventPublisher.Publish(entity);

            return result.Adapt<ItemDto>();
        }
    }
}
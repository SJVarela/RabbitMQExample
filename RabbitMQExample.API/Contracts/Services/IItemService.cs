using RabbitMQExample.API.Models;

namespace RabbitMQExample.API.Contracts.Services
{
    public interface IItemService
    {
        ItemDto SaveItem(ItemDto item);
    }
}
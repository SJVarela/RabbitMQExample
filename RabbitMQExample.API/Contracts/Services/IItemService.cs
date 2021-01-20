using RabbitMQExample.API.Models;
using System.Collections.Generic;

namespace RabbitMQExample.API.Contracts.Services
{
    public interface IItemService
    {
        IEnumerable<ItemDto> GetItems();

        ItemDto SaveItem(ItemDto item);
    }
}
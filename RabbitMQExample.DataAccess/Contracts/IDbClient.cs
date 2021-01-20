using RabbitMQExample.DataAccess.Models.Core;
using System.Collections.Generic;

namespace RabbitMQExample.DataAccess.Contracts
{
    public interface IDbClient<T> where T : Entity
    {
        T Create(T item);

        IEnumerable<T> GetItems();
    }
}
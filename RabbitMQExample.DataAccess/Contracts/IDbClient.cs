using RabbitMQExample.DataAccess.Models.Core;

namespace RabbitMQExample.DataAccess.Contracts
{
    public interface IDbClient<T> where T : Entity
    {
        T Create(T item);
    }
}
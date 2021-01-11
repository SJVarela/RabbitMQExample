using MongoDB.Bson;
using MongoDB.Driver;
using RabbitMQExample.DataAccess.Config;
using RabbitMQExample.DataAccess.Contracts;
using RabbitMQExample.DataAccess.Models.Core;
using System.Collections.Generic;

namespace RabbitMQExample.DataAccess.Access
{
    public class DbClient<T> : IDbClient<T> where T : Entity
    {
        private readonly IMongoCollection<T> _collection;

        public DbClient(IDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            if (!database.ListCollections(new ListCollectionsOptions { Filter = new BsonDocument("name", typeof(T).Name) }).Any())
            {
                database.CreateCollection(typeof(T).Name);
            }
            _collection = database.GetCollection<T>(typeof(T).Name);
        }

        public T Create(T item)
        {
            _collection.InsertOne(item);
            return item;
        }

        public IEnumerable<T> GetItems()
        {
            return _collection.Find(x => true).ToList();
        }
    }
}
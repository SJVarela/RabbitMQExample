using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RabbitMQExample.DataAccess.Models.Core
{
    public abstract class Entity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
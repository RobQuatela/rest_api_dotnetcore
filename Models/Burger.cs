using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace rest_api_dotnetcore.Models
{
    public class Burger
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("price")]
        public decimal Price { get; set; }

    }
}
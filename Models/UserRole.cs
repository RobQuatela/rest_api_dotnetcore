using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace rest_api_dotnetcore.Models
{
    public class UserRole
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("user_id")]
        public string UserId { get; set; }
        public virtual User User { get; set; }
        [BsonElement("role_id")]
        public string RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CMS.Models
{
    [Serializable, BsonIgnoreExtraElements]
    public class User
    {
        [BsonId, BsonRepresentation(BsonType.ObjectId)]
        public string? UserId { get; set; }

        [BsonElement("firstName"), BsonRepresentation(BsonType.String)]
        public string firstName { get; set; }

        [BsonElement("lastName"), BsonRepresentation(BsonType.String)]
        public string lastName { get; set; }

        [BsonElement("email"), BsonRepresentation(BsonType.String)]
        public string Email { get; set; }

        [BsonElement("role"), BsonRepresentation(BsonType.String)]
        public string Role { get; set; }

        [BsonElement("team"), BsonRepresentation(BsonType.String)]
        public string Team { get; set; }
    }
}


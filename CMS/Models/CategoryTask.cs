using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CMS.Models
{
    [Serializable, BsonIgnoreExtraElements]

    public class CategoryTasks
    {
        [BsonElement("0"), BsonRepresentation(BsonType.String)]
        public string Category { get; set; } = null!;


    }
}

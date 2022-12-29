using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CMS.Models
{

    [Serializable, BsonIgnoreExtraElements]

    public class Competition
    {
        [BsonId, BsonRepresentation(BsonType.ObjectId)] //Name, Number of parallel tasks, SM code, List of tasks.

        public string? CompetitionId { get; set; }

        [BsonElement("name"), BsonRepresentation(BsonType.String)]
        public string Name { get; set; }

        [BsonElement("numberOfParallel"), BsonRepresentation(BsonType.Int32)]
        public int NumberOfParallel { get; set; }

        [BsonElement("SMcode"), BsonRepresentation(BsonType.Int32)]
        public int SMcode { get; set; }

        [BsonElement("listOfTasks")]
        public string[] ListOfTasks { get; set; }
    }
}

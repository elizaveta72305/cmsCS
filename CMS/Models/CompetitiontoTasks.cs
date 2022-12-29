using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CMS.Models
{
    public class CompetitionToTasks
    {
        [BsonId, BsonRepresentation(BsonType.ObjectId)] 
        public string Id { get; set; }

        [BsonElement("competitionId"), BsonRepresentation(BsonType.ObjectId)]
        public Object Competition { get; set; }

        [BsonElement("taskId"), BsonRepresentation(BsonType.Array)]
        public Object Task { get; set; }
    }
}

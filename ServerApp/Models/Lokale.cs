using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ServerApp.Models
{
    public class Lokale
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }
        public string Location { get; set; }
        public string Åbningstid { get; set; }
        public string Bemanding { get; set; }
        public string Type { get; set; }
        public string Adgang { get; set; }
    }
}
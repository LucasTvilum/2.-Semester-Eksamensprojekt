using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ServerApp.Models
{
    public class Bruger
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }
        public string Password { get; set; }
    }
}
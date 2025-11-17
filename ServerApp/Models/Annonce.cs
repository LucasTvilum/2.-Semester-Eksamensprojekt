using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ServerApp.Models
{
    public class Annonce
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Description { get; set; }
        public double Price { get; set; }
        public string Type { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public string Image { get; set; }
        public string Status { get; set; }
        public string Sælger_Id { get; set; }
        public string Køber_Id { get; set; }
    }
}

namespace Core.Models
{
    public class Annonce
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Description { get; set; }
        public double Price { get; set; }
        public string Type { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public string Image { get; set; }
        public string Status { get; set; }
        public string Sælger_Id { get; set; }
        public string Køber_Id { get; set; }
        public string Bruger_Id { get; set; } = Guid.NewGuid().ToString();
        public Bruger bruger { get; set; }
        public string Lokale_Id { get; set; } = Guid.NewGuid().ToString();
        public Lokale lokale { get; set; }
    }
}
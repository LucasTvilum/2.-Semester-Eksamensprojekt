
namespace Core.Models
{
    public class Annonce
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public double Price { get; set; } = 0.0;
        public string Type { get; set; } = "tøj";
        public string Size { get; set; } = "M";
        public string Color { get; set; } = "red";
        public string Image { get; set; } = "";
        public string Status { get; set; } = "";
        public string Sælger_Id { get; set; } = "";
        public string Køber_Id { get; set; } = "";
        public string Lokale_Id { get; set; } = Guid.NewGuid().ToString();

        public Lokale lokale { get; set; } = new Lokale
            { Name = "hej", Location = "A1", Åbningstid = "ja", Bemanding = "ole", Type = "Stor", Adgang = "ja" };
    }
}
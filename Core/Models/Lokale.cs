
namespace Core.Models
{
    public class Lokale
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; } = "";
        public string Location { get; set; } = "";
        public string Åbningstid { get; set; } = "";
        public string Bemanding { get; set; } = "";
        public string Type { get; set; } = "normal";
        public string Adgang { get; set; } = "åben";
    }
}
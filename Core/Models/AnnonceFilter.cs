namespace Core.Models;

public class AnnonceFilter 
{
    public string Type { get; set; }
    public string Size { get; set; }
    public double Price { get; set; }
    public string Color { get; set; }
    
    public Lokale lokale { get; set; } = new Lokale
        { Name = "hej", Location = "A1", Åbningstid = "ja", Bemanding = "ole", Type = "Stor", Adgang = "ja" };
    
    
}
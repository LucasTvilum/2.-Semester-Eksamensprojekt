namespace Core.Models;

//Kan eventuelt flyttes ind i Window modelklasse, men så kan den kun bruges via window objekt

public class WindowLocation
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = "";         // e.g. "Stue", "1. sal"
    public decimal ExtraPrice { get; set; } = 0;    // price modifier
    public bool IsActive { get; set; } = true;
}
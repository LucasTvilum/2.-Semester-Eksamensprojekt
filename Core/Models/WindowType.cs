namespace Core.Models;


//Kan eventuelt flyttes ind i Window modelklasse, men så kan den kun bruges via window objekt

public class WindowType
{
public string Id { get; set; } = Guid.NewGuid().ToString();
public string Name { get; set; } = "";         // e.g. "Dannebrog"
public string ImagePath { get; set; } = "";
public decimal BasePrice { get; set; } = 0;     // admin can edit
public bool IsActive { get; set; } = true; // soft delete support
}
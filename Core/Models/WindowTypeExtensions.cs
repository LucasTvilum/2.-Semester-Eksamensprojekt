namespace Core.Models;

//Kan eventuelt flyttes ind i Window modelklasse, men så kan den kun bruges via window objekt


public static class WindowTypeExtensions
{
    public static string GetLabel(this WindowType type) =>
        type switch
        {
            WindowType.Dannebrog => "Dannebrogs vindue",
            WindowType.Bondehus => "Bondehus vindue",
            WindowType.Matteret => "Matteret vindue",
            WindowType.Palae => "Palæ vindue",
            WindowType.Sidehaengt => "Sidehængt vindue",
            WindowType.Standard => "Normalt vindue",
            WindowType.Stort => "Stort vindue",
            _ => "Unknown"
        };
    
    public static int GetPriceLocation(this WindowType loc) =>
        loc switch
        {
            WindowType.Dannebrog => 50,
            WindowType.Bondehus => 50,
            WindowType.Matteret => 50,
            WindowType.Palae => 50,
            WindowType.Sidehaengt => 50,
            WindowType.Standard => 50,
            WindowType.Stort => 50,
            _ => 999
        };
}
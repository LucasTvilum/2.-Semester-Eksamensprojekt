namespace Core.Models;

public class Window
{
    public string WindowId { get; set; } = Guid.NewGuid().ToString();
    public WindowType Type { get; set; }
    public WindowLocation Location { get; set; }
    
    public decimal Price{ get; set; } // beregnet ud fra type x location
    
    public static string GetLabelLocation(WindowLocation loc) =>
        loc switch
        {
            WindowLocation.StueEtage => "Stue Etage",
            WindowLocation.FoersteEtage => "Første Etage",
            WindowLocation.Indenfor => "Indenfor",
            WindowLocation.Kaelder => "Kælder",
            _ => "Unknown"
        };
    
    public static int GetPriceLocationMultiplier(WindowLocation loc) =>
        loc switch
        {
            WindowLocation.StueEtage => 1,
            WindowLocation.FoersteEtage => 2,
            WindowLocation.Indenfor => 3,
            WindowLocation.Kaelder => 1,
            _ => 999
        };
    
    public static string GetLabelType(WindowType type) =>
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
    
    public static int GetPriceType(WindowType loc) =>
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
    public decimal CalculatePrice()
    {
        int basePrice = GetPriceType(Type);
        int multiplier = GetPriceLocationMultiplier(Location);
        Price = basePrice * multiplier;
        return Price;
    }
}

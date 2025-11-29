namespace Core.Models;

//Kan eventuelt flyttes ind i Window modelklasse, men så kan den kun bruges via window objekt

public static class WindowLocationExtensions
{
    public static string GetLabel(this WindowLocation loc) =>
        loc switch
        {
            WindowLocation.StueEtage => "Stue Etage",
            WindowLocation.FoersteEtage => "Første Etage",
            WindowLocation.Indenfor => "Indenfor",
            WindowLocation.Kaelder => "Kælder",
            _ => "Unknown"
        };
    
    public static int GetPriceLocation(this WindowLocation loc) =>
        loc switch
        {
            WindowLocation.StueEtage => 50,
            WindowLocation.FoersteEtage => 50,
            WindowLocation.Indenfor => 50,
            WindowLocation.Kaelder => 50,
            _ => 999
        };
}
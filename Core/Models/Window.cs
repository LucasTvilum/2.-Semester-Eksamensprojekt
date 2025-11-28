namespace Core.Models;

public class Window
{
    public string WindowId { get; set; } = Guid.NewGuid().ToString();
    
    //Kan rykke ud i egen modelklasse hvis informationen skal bruges andre steder
    public class WindowType
    {
        public string Value { get; set; }
        
        //Public og private windowtype for json deserialization, kan være skal ændres
        public WindowType() {}
        private WindowType(string v) { Value = v; }

        public static readonly WindowType Type1 = new("Standardvindue");
        public static readonly WindowType Type2 = new("Sidehængtvindue");
        public static readonly WindowType Type3 = new("Dannebrogsvindue");
        public static readonly WindowType Type4 = new("Bondehusvindue");
        public static readonly WindowType Type5 = new("Stortvindue");
        public static readonly WindowType Type6 = new("Palævindue");
        public static readonly WindowType Type7 = new("Matteretvindue");
        public static readonly WindowType Type8 = new("Type3");
        public static readonly WindowType Type9 = new("Type3");
        public static readonly WindowType Type10 = new("Type3");
    }
    
    //Kan rykke ud i egen modelklasse hvis informationen skal bruges andre steder
    public class WindowLocation
    {
        public string Value { get; set; }
        
        
        //hurtig fix for at få json deserialization til at virke, skal nok flyttes til backend
        public WindowLocation() {}
        private WindowLocation(string v) { Value = v; }

        public static readonly WindowLocation StueEtage = new("StueEtage");
        public static readonly WindowLocation FørsteEtage = new("FørsteEtage");
        public static readonly WindowLocation Indenfor = new("Indenfor");
        public static readonly WindowLocation Kælder = new("Kælder");
    }
    public WindowType Type { get; set; }
    public WindowLocation Location { get; set; }
    public decimal Pris { get; set; } // beregnet ud fra type x location
}

namespace Core.Models;

public class Window
{
    public string WindowId { get; set; } = Guid.NewGuid().ToString();
    public WindowType Type { get; set; }
    public WindowLocation Location { get; set; }
    
    public decimal Price{ get; set; } // beregnet ud fra type x location
}

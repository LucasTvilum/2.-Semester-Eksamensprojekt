namespace Core.Models;

public class Window
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public WindowType Type { get; set; }
    public WindowLocation Location { get; set; }

    public decimal Price { get; set; } // beregnet ud fra type.baseprice x location.extraprice

  

    public decimal CalculatePrice()
    {
        decimal basePrice = Type.BasePrice;
        decimal multiplier = Location.ExtraPrice;
        Price = basePrice * multiplier;
        return Price;
    }
}

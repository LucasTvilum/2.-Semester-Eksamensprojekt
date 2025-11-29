namespace Core.Models;

public class WindowList
{
    public List<Window> Windows { get; set; } = new List<Window>();
    public decimal TotalPrice { get; private set; }

    // Konstruktør
    public WindowList(List<Window> windows)
    {
        //?? is the null-coalescing operator in C#.It means: If windows is not null, use it.  If windows is null, create a new empty List<Window>().
        Windows = windows ?? new List<Window>();
        CalculateTotalPrice();
    }
    // SamletPris
    private void CalculateTotalPrice()
    {
        TotalPrice = 0;
        foreach (var window in Windows)
        {
            // Make sure individual window prices are calculated
            TotalPrice += window.CalculatePrice();
        }
    }
    
}
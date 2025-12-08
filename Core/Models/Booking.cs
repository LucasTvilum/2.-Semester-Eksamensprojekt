namespace Core.Models;

public class Booking
{
    public string Id { get; set; } = Guid.NewGuid().ToString(); //PK
    public string CustomerId { get; set; } = "";// FK til Kunde
    public string Status { get; set; } = "";
    public decimal Price { get; set; }
    
    public DateTime Date { get; set; } = DateTime.Now.Date; //hvis enkeltbooking så skal den bruges
    public string Day { get; set; } = DateTime.Now.ToString("dddd");

    public List<Window> Windows { get; set; } = new List<Window>();
    public BookingType TypeBooking { get; set; } = BookingType.EnkeltBooking;
    public string NotesCustomer { get; set; } = "";
    public string NotesWindowCleaner { get; set; } = "";
    public bool InsideJob { get; set; }
    public int OutdoorInterval { get; set; }
    public int InsideInterval { get; set; }
    
    public enum BookingType {
        Abonnement,
        EnkeltBooking
    }
    
    public static string GetLabel(BookingType btype) =>
        btype switch
        {
            BookingType.Abonnement => "Abonnement",
            BookingType.EnkeltBooking => "Enkelt booking",
            _ => "Unknown"
        };
}

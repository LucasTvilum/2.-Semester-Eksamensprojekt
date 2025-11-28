namespace Core.Models;

public class Booking
{
    public string BookingId { get; set; } = Guid.NewGuid().ToString(); //PK
    public string CustomerId { get; set; } // FK til Kunde
    public string Status { get; set; }
    public decimal Price { get; set; }
    public string Day { get; set; }

    public List<Window> Windows { get; set; } = new List<Window>();
    public string TypeBooking { get; set; }
    public string NotesCustomer { get; set; }
    public string NotesWindowCleaner { get; set; }
    public bool InsideJob { get; set; }

    public List<WorkTask> WorkTasks { get; set; } = new List<WorkTask>();

    public string OutdoorInterval { get; set; }
    public string InsideInterval { get; set; }
}

namespace Core.Models;

public class WorkTask
{
    public string WorkTaskId { get; set; } = Guid.NewGuid().ToString();
    public string BookingId {get; set;} // FK
    public List<Window> Windows { get; set; } = new List<Window>(); //Fra Booking
    public DateTime Date { get; set; }
    public bool InsideJob { get; set; }
    public Worker Worker { get; set; }
    public string NotesForTask { get; set; }
}

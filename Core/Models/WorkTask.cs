namespace Core.Models;

public class WorkTask
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string BookingId {get; set;} // FK
    
    public string WorkerId { get; set; } // FK
    public DateTime Date { get; set; }
    public bool InsideJob { get; set; }
    

    public string Status { get; set; } = "";

}

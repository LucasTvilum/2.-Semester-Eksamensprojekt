namespace Core.Models;

public class Worker: User
{
    public string WorkerId { get; set; } = Guid.NewGuid().ToString();
    public bool Admin { get; set; } = true;
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Mail { get; set; }
}

namespace Core.Models;

public class Customer : User
{
    public string Name { get; set; } = "";
    public string Mail { get; set; } = "";
    public string PhoneNumber { get; set; } = "";
    public bool Subscription { get; set; } = false;
    public string Address { get; set; } = "";
    public string Region { get; set; } = "";
    public string City { get; set; } = "";
}

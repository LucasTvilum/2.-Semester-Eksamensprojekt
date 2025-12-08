namespace Core.Models;

public class Customer : User
{
    public Customer()
    {
        Usertype = UserType.Customer;
    }
    
    public bool Subscription { get; set; } = false;
    public string Address { get; set; } = "";
    public string Region { get; set; } = "";
    public string City { get; set; } = "";
}

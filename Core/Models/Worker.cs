namespace Core.Models;

public class Worker: User
{ 
    public Worker()
    {
        Usertype = UserType.Worker;
    }
    public bool Admin { get; set; } = true;

}

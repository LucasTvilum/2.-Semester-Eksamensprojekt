namespace Core.Models;

public class User
{
    public string UserId { get; set; } = Guid.NewGuid().ToString();
    public string Username { get; set; }
    public string Password { get; set; }
}

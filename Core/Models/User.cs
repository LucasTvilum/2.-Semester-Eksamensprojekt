namespace Core.Models;

public class User
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    public UserType Usertype { get; set; } = UserType.User;
    public string Username { get; set; }
    public string Password { get; set; }
    
    public enum UserType
    {
        User,
        Customer,
        Worker
    }
   
    public static string GetLabel(UserType type) => type switch
    {
        UserType.User => "User",
        UserType.Customer => "Customer",
        UserType.Worker => "Worker",
        _ => "User"
    };
    
    public static UserType ParseUserType(int value)
    {
        return Enum.IsDefined(typeof(UserType), value)
            ? (UserType)value
            : UserType.User; // fallback
    }
    
}

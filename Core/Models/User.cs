namespace Core.Models;

public class User
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    public UserType Usertype { get; set; } = UserType.User;
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
    public string Name { get; set; } = "";
    public string Mail { get; set; } = "";
    public string PhoneNumber { get; set; } = "";
    
    
    public enum UserType
    {
        User = 0,
        Customer = 1,
        Worker = 2
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

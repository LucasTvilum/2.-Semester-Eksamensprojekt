using Core.Models;
namespace ClientApp.Service;

public class UserState
{
    public User? CurrentUser { get; set; }

    public bool IsLoggedIn => CurrentUser != null;

    public bool IsCustomer => CurrentUser?.Usertype == User.UserType.Customer;
    public bool IsWorker => CurrentUser?.Usertype == User.UserType.Worker;
}
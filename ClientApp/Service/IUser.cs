using Core.Models;

namespace ClientApp.Service;


public interface IUser
{
    Task<User[]> GetAll();

    Task Add(User user);
    
    Task Delete(string userid);
    
    Task<User> ValidateUser(User user);
}
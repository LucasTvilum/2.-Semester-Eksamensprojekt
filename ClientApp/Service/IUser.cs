using Core.Models;

namespace ClientApp.Service;


public interface IUser
{
    Task<User[]> GetAll();

    Task Add(User user);
    
    Task<Customer> GetCustomerById(string userid);
    
    Task Delete(string userid);
    
    Task<User> ValidateUser(User user);

    Task<List<Customer>> GetCustomers();

    Task<List<Worker>> GetWorkers();
}
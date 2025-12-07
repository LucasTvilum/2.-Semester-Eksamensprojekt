namespace ServerApp.Repository;
using Core.Models;

public interface IUserRepository

{
    List<User> GetAll();
    
    void Add(User user);
    
    User Update(User user);
    
    void Delete(string id);

    Task<List<Customer>> GetCustomers();

    Task<List<Worker>> GetWorkers();
}
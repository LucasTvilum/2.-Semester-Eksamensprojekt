namespace ServerApp.Repository;
using Core.Models;

public interface ICustomerRepository

{
    List<Customer> GetAll();
    
    void Add(Customer customer);
    
    Customer Update(Customer customer);
    
    void Delete(string id);
}
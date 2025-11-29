using Core.Models;

namespace ClientApp.Service;


public interface ICustomer
{
    Task<Customer[]> GetAll();

    Task Add(Customer customer);
    
    Task UpdateBooking(Customer customer);

    Task Delete(string userid);
}
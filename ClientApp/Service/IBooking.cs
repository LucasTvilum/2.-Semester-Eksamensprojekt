using Core.Models;

namespace ClientApp.Service;


public interface IBooking
{
    Task<Booking[]> GetAll();

    Task Add(Booking booking);
    
    Task UpdateBooking(Booking booking);

    Task Delete(string todoid);
}
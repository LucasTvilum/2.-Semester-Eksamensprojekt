namespace ServerApp.Repository;
using Core.Models;

public interface IBookingRepository

{
    List<Booking> GetAll();
    
    void Add(Booking booking);
    
    Booking Update(Booking booking);
    
    void Delete(string id);
}
namespace ServerApp.Repository;
using Core.Models;

public interface IBookingRepository

{
    List<Booking> GetAll();
    
    void Add(Booking annonce);
    
    Booking Update(Booking annonce);
    
    void Delete(string id);
}
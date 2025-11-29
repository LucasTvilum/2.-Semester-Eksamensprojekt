namespace ServerApp.Repository;
using Core.Models;

public interface IBookingInterface

{
    List<Booking> GetAll();
    
    void Add(Booking annonce);
    
    Booking Update(Booking annonce);
    
    void Delete(string id);
}
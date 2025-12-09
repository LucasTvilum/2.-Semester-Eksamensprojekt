using Core.Models;

namespace ClientApp.Service;


public interface IBooking
{
    Task<Booking[]> GetAll();
    
    Task<Booking> GetBookingById(string bookingid);

    Task Add(Booking booking);
    
    Task UpdateBooking(Booking booking);

    Task Delete(string bookingid);
}
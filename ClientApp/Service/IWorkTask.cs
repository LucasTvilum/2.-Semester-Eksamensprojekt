using Core.Models;
namespace ClientApp.Service;

public interface IWorkTask
{
    Task<WorkTask[]> GetAll();

    Task Add(WorkTask task);
    
    Task<WorkTask> GetByBookingId(string bookingid);

    Task AddSubscription(Booking booking);
    
    Task AddSingleBooking(Booking booking);
    
    Task UpdateWorkTask(WorkTask task);

    Task DeleteAllWorkTaskFromBookingId(string bookingid);

    Task Delete(string taskid);
}
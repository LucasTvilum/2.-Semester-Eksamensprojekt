using Core.Models;
namespace ClientApp.Service;

public interface IWorkTask
{
    Task<WorkTask[]> GetAll();

    Task Add(WorkTask task);

    Task AddSubscription(Booking booking);
    
    Task AddSingleBooking(Booking booking);
    
    Task UpdateWorkTask(WorkTask task);

    Task Delete(string taskid);
}
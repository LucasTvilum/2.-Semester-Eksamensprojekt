namespace ServerApp.Repository;
using Core.Models;

public interface IWorkTaskRepository

{
    List<WorkTask> GetAll();
    
    void Add(WorkTask task);
    
    void AddSubscription(List<WorkTask> workTasks);

    
    WorkTask Update(WorkTask task);
    
    void Delete(string id);
    
    void DeleteAllWorktasksFromBookingId(string bookingid);
}
using Core.Models;
namespace ClientApp.Service;

public interface IWorkTask
{
    Task<WorkTask[]> GetAll();

    Task Add(WorkTask task);
    
    Task UpdateWorkTask(WorkTask task);

    Task Delete(string taskid);
}
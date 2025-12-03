using Core.Models;
namespace ServerApp.Repository;

public interface IWindowLocationRepository
{
    List<WindowLocation> GetAll();
    
    void Add(WindowLocation windowLocation);
    
    WindowLocation Update(WindowLocation windowLocation);
    
    void Delete(string id);
}
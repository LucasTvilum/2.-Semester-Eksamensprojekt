using Core.Models;
namespace ServerApp.Repository;

public interface IWindowTypeRepository
{
    List<WindowType> GetAll();
    
    void Add(WindowType windowtype);
    
    WindowType Update(WindowType windowtype);
    
    void Delete(string id);
}
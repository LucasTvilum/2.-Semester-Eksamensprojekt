using Core.Models;

namespace ClientApp.Service;


public interface IWindow
{
    Task<Window[]> GetAll();
    Task<List<WindowType>> GetAllWindowTypes();
    Task<List<WindowLocation>> GetAllWindowLocations();
    
    
    Task AddType(WindowType windowtype);
    
    Task AddLocation(WindowLocation windowlocation);
    Task Add(Window window);
    
    Task UpdateWindow(Window window);

    Task Delete(string windowid);
    
    Task UpdateType(WindowType type);
    Task UpdateLocation(WindowLocation location);

    Task DeleteType(string id);
    Task DeleteLocation(string id);
}
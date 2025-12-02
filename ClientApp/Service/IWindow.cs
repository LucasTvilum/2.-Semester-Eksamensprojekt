using Core.Models;

namespace ClientApp.Service;


public interface IWindow
{
    Task<Window[]> GetAll();
    
    Task<WindowList> GetWindowList();
    Task<List<WindowType>> GetAllWindowTypes();
    Task<List<WindowLocation>> GetAllWindowLocations();
    
    
    Task AddType(WindowType windowtype);
    
    Task AddLocation(WindowLocation windowlocation);
    Task Add(Window window);
    
    Task UpdateWindow(Window window);

    Task Delete(string windowid);
}
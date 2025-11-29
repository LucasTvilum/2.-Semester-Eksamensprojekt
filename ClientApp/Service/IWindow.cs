using Core.Models;

namespace ClientApp.Service;


public interface IWindow
{
    Task<Window[]> GetAll();
    
    Task<WindowList> GetWindowList();

    Task Add(Window window);
    
    Task UpdateBooking(Window window);

    Task Delete(string todoid);
}
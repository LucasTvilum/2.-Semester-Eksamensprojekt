using Core.Models;

namespace ClientApp.Service;


public interface IWindow
{
    Task<Window[]> GetAll();
    
    Task<WindowList> GetWindowList();

    Task Add(Window window);
    
    Task UpdateWindow(Window window);

    Task Delete(string windowid);
}
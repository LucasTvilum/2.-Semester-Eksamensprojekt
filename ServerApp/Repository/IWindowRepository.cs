namespace ServerApp.Repository;
using Core.Models;

public interface IWindowRepository

{
    List<Window> GetAll();
    
    WindowList GetWindowList();
    
    void Add(Window window);
    
    Window Update(Window window);
    
    void Delete(string id);
}
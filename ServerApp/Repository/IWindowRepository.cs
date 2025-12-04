namespace ServerApp.Repository;
using Core.Models;

public interface IWindowRepository

{
    List<Window> GetAll();
    void Add(Window window);
    
    Window Update(Window window);
    
    void Delete(string id);
}
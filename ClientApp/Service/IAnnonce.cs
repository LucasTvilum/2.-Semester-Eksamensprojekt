using Core.Models;

namespace ClientApp.Service;


public interface IAnnonce
{
    Task<Annonce[]> GetAll();

    Task Add(Annonce item);
    
    Task UpdateAnnonce(Annonce item);

    Task Delete(string todoid);
    
    Task <List<Annonce>> GetFiltered(string type, string size, double price, string color, string lokalenavn);
}
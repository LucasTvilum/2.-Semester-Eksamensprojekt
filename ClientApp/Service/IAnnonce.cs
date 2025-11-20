using Core.Models;

namespace ClientApp.Service;


public interface IAnnonce
{
    Task<Annonce[]> GetAll();

    Task Add(Annonce item);
    
    Task UpdateAnnonce(Annonce item);

    Task Delete(string todoid);
}
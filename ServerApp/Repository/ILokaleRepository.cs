namespace ServerApp.Repository;
using Core.Models;

public interface ILokaleRepository
{
    List<Lokale> GetAll();
    
    Lokale Add(Lokale lokale);
    
    Lokale Update(Lokale lokale);
    
    void Delete(Lokale lokale);
    
}
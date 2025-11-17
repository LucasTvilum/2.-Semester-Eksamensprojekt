namespace ServerApp.Repository;
using Core.Models;

public interface IBrugerRepository
{
    List<Bruger> GetAll();
    
    Bruger Add(Bruger bruger);
    
    Bruger Update(Bruger bruger);
    
    void Delete(Bruger bruger);
    
}
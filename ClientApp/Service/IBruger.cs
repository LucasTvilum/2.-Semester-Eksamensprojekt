using Core.Models;

namespace ClientApp.Service;

public interface IBruger
    
{ 
        Task<Bruger[]> GetAll();

        Task Add(Bruger bruger);

        Task Delete(string todoid);
        
        Task<Bruger> Authenticate(string username, string password);
}
using Core.Models;

namespace ClientApp.Service;

public interface IAnnonce
{


    public interface IAnnonce
    {
        Task<Annonce[]> GetAll();

        Task Add(Annonce item);

        Task Delete(string todoid);
    }
}
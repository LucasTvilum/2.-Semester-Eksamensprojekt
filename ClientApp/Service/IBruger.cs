using Core.Models;

namespace ClientApp.Service;

public interface IBruger
{


    public interface IBruger
    {
        Task<Annonce[]> GetAll();

        Task Add(Annonce item);

        Task Delete(string todoid);
    }
}
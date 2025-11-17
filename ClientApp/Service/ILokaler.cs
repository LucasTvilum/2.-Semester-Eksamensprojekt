using Core.Models;

namespace ClientApp.Service;

public interface ILokaler
{


    public interface ILokaler
    {
        Task<Annonce[]> GetAll();

        Task Add(Annonce item);

        Task Delete(string todoid);
    }
}
using Core.Models;

namespace ClientApp.Service;

    public interface ILokaler
    {
        Task<List<Lokale>> GetAll();

        Task Add(Lokale item);

        Task Delete(string lokaleid);
        
        Task UpdateTime(Lokale lokale, string time);
    }
namespace ServerApp.Repository;
using Core.Models;

public interface IAnnonceRepository
{
    List<Annonce> GetAll();
    
    Annonce Add(Annonce annonce);
    
    Annonce Update(Annonce annonce);
    
    void Delete(string id);
    
}
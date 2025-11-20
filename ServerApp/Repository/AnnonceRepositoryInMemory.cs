using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Models;

namespace ServerApp.Repository;


internal class AnnonceRepositoryInMemory : IAnnonceRepository
{
    private readonly List<Annonce> annoncer = new();

    public List<Annonce> GetAll()
    {
        return annoncer;
    }

    public Annonce Add(Annonce annonce)
    {
        annoncer.Add(annonce);
        return annonce;
    }

    public Annonce Update(Annonce annonce)
    {
        var existingItem = annoncer.FirstOrDefault(t => t.Id == annonce.Id);
        if (existingItem != null)
        {
            existingItem.Description = annonce.Description;
            existingItem.Status = annonce.Status;
        }

        return existingItem!;
    }

    public void Delete(string id)
    {
        annoncer.RemoveAll(t => t.Id == id);
    }

    public List<Annonce> GetFiltered(AnnonceFilter filter)
    {
        var query = annoncer.AsQueryable();

        if (!string.IsNullOrEmpty(filter.Type))
            query = query.Where(a => a.Type == filter.Type);
        
        if (!string.IsNullOrEmpty(filter.Size))
            query = query.Where(a => a.Size == filter.Size);

        if (filter.Price > 0)
            query = query.Where(a => a.Price == filter.Price);

        if (!string.IsNullOrEmpty(filter.Color))
            query = query.Where(a => a.Color == filter.Color);

        if (!string.IsNullOrEmpty(filter.lokale.Name))
            query = query.Where(a => a.lokale.Name == filter.lokale.Name);


        return query.ToList();


    }
}

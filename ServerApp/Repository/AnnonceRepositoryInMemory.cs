using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Models;

namespace ServerApp.Repository;

internal class AnnonceRepositoryInMemory : IAnnonceRepository
{
    private readonly List<Annonce> todos = new();

    public List<Annonce> GetAll()
    {
        return todos;
    }

    public Annonce Add(Annonce annonce)
    {
        todos.Add(annonce);
        return annonce;
    }

    public Annonce Update(Annonce annonce)
    {
        var existingItem = todos.FirstOrDefault(t => t.Id == annonce.Id);
        if (existingItem != null)
        {
            existingItem.Description = annonce.Description;
            existingItem.Status = annonce.Status;
        }

        return existingItem!;
    }

    public void Delete(string id)
    {
        todos.RemoveAll(t => t.Id == id);
    }
}
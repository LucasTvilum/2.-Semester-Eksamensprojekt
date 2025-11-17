using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Models;

namespace ServerApp.Repository;

internal class LokaleRepositoryInMemory : ILokaleRepository
{
    private readonly List<Lokale> todos = new();

    public List<Lokale> GetAll()
    {
        return todos;
    }

    public Lokale Add(Lokale lokale)
    {
        todos.Add(lokale);
        return lokale;
    }

    public Lokale Update(Lokale lokale)
    {
        var existingItem = todos.FirstOrDefault(t => t.Id == lokale.Id);
        if (existingItem != null)
        {
            existingItem.Title = lokale.Title;
            existingItem.IsDone = lokale.IsDone;
        }

        return existingItem!;
    }

    public void Delete(Lokale lokale)
    {
        todos.RemoveAll(t => t.Id == lokale.Id);
    }
}
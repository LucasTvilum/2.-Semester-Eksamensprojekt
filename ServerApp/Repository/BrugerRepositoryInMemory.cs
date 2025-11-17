using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Models;

namespace ServerApp.Repository;

internal class BrugerRepositoryInMemory : IBrugerRepository
{
    private readonly List<Bruger> todos = new();

    public List<Bruger> GetAll()
    {
        return todos;
    }

    public Bruger Add(Bruger bruger)
    {
        todos.Add(bruger);
        return bruger;
    }

    public Bruger Update(Bruger bruger)
    {
        var existingItem = todos.FirstOrDefault(t => t.Id == bruger.Id);
        if (existingItem != null)
        {
            existingItem.Title = bruger.Title;
            existingItem.IsDone = bruger.IsDone;
        }

        return existingItem!;
    }

    public void Delete(Bruger bruger)
    {
        todos.RemoveAll(t => t.Id == bruger.Id);
    }
}
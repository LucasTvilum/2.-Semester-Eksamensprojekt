using MongoDB.Driver;
using Core.Models;
using ServerApp.Repository;

namespace ServerApp.Repository;
public class WindowMongoDB : IWindowRepository
{
    private readonly IMongoCollection<Window> _collection;

    public WindowMongoDB()
    {
        var client = new MongoClient("mongodb://localhost:27017");
        var database = client.GetDatabase("Vinduespudsning");
        _collection = database.GetCollection<Window>("Windows");
    }

    public List<Window> GetAll()
    {
        return _collection.Find(_ => true).ToList();
    }
    
    public WindowList GetWindowList()
    {
        WindowList windowlist = new WindowList(_collection.Find(_ => true).ToList());
        return windowlist;
    }

    public void Add(Window window)
    {
        _collection.InsertOne(window);
    }

    public Window Update(Window window)
    {
        var filter = Builders<Window>.Filter.Eq(a => a.Id, window.Id);
        _collection.ReplaceOne(filter, window);
        return window;
    }

    public void Delete(string id)
    {
        _collection.DeleteOne(id);
    }

}
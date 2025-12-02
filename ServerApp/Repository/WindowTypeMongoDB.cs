using MongoDB.Driver;
using Core.Models;
using ServerApp.Repository;

namespace ServerApp.Repository;
public class WindowTypeMongoDB : IWindowTypeRepository
{
    private readonly IMongoCollection<WindowType> _collection;

    public WindowTypeMongoDB()
    {
        var client = new MongoClient("mongodb://localhost:27017");
        var database = client.GetDatabase("Vinduespudsning");
        _collection = database.GetCollection<WindowType>("WindowTypes");
    }

    public List<WindowType> GetAll()
    {
        return _collection.Find(_ => true).ToList();
    }
    
    public void Add(WindowType windowType)
    {
        _collection.InsertOne(windowType);
    }

    public WindowType Update(WindowType windowType)
    {
        var filter = Builders<WindowType>.Filter.Eq(a => a.Id, windowType.Id);
        _collection.ReplaceOne(filter, windowType);
        //tror at den bare returnerer objektet man gav den atm
        return windowType;
    }

    public void Delete(string id)
    {
        _collection.DeleteOne(id);
    }

}
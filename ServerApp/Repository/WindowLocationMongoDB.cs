using MongoDB.Driver;
using Core.Models;
using ServerApp.Repository;

namespace ServerApp.Repository;
public class WindowLocationMongoDB : IWindowLocationRepository
{
    private readonly IMongoCollection<WindowLocation> _collection;

    public WindowLocationMongoDB()
    {
        var client = new MongoClient("mongodb://localhost:27017");
        var database = client.GetDatabase("Vinduespudsning");
        _collection = database.GetCollection<WindowLocation>("WindowLocations");
    }

    public List<WindowLocation> GetAll()
    {
        return _collection.Find(_ => true).ToList();
    }
    
    public void Add(WindowLocation windowlocation)
    {
        _collection.InsertOne(windowlocation);
    }

    public WindowLocation Update(WindowLocation windowlocation)
    {
        var filter = Builders<WindowLocation>.Filter.Eq(a => a.Id, windowlocation.Id);
        _collection.ReplaceOne(filter, windowlocation);
        //tror at den bare returnerer objektet man gav den atm
        return windowlocation;
    }

    public void Delete(string id)
    {
        _collection.DeleteOne(id);
    }

}
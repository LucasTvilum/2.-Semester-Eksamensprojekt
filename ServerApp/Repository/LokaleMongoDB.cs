using MongoDB.Driver;
using Core.Models;
using ServerApp.Repository;


public class LokaleMongoDB : ILokaleRepository
{
    private readonly IMongoCollection<Lokale> _collection;

    public LokaleMongoDB()
    {
        var  client = new MongoClient("mongodb://localhost:27017");
        var database = client.GetDatabase("TøjmarkedMongoDB");
        _collection = database.GetCollection<Lokale>("TøjmarkedMongoDB");
    }

    public List<Lokale> GetAll()
    {
        return _collection.Find(_ => true).ToList();
    }

    public Lokale Add(Lokale lokale)
    {
        _collection.InsertOne(lokale);
        return lokale;
    }

    public Lokale Update(Lokale lokale)
    {
        _collection.ReplaceOne(lokale.Id, lokale);
        return lokale;
    }

    public void Delete(Lokale lokale)
    {
        _collection.DeleteOne(lokale.Id);
    }
}
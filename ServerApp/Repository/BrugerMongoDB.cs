using MongoDB.Driver;
using Core.Models;
using ServerApp.Repository;


public class BrugerMongoDB : IBrugerRepository
{
    private readonly IMongoCollection<Bruger> _collection;

    public BrugerMongoDB()
    {
        var  client = new MongoClient("mongodb://localhost:27017");
        var database = client.GetDatabase("TøjmarkedMongoDB");
        _collection = database.GetCollection<Bruger>("TøjmarkedMongoDB");
    }

    public List<Bruger> GetAll()
    {
        return _collection.Find(_ => true).ToList();
    }

    public Bruger Add(Bruger bruger)
    {
        _collection.InsertOne(bruger);
        return bruger;
    }

    public Bruger Update(Bruger bruger)
    {
        _collection.ReplaceOne(bruger.Id, bruger);
        return bruger;
    }

    public void Delete(Bruger bruger)
    {
        _collection.DeleteOne(bruger.Id);
    }
}
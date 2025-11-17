using MongoDB.Driver;
using Core.Models;
using ServerApp.Repository;


public class AnnonceMongoDB : IAnnonceRepository
{
    private readonly IMongoCollection<Annonce> _collection;

    public AnnonceMongoDB()
    {
        var  client = new MongoClient("mongodb://localhost:27017");
        var database = client.GetDatabase("TøjmarkedMongoDB");
        _collection = database.GetCollection<Annonce>("TøjmarkedMongoDB");
    }

    public List<Annonce> GetAll()
    {
        return _collection.Find(_ => true).ToList();
    }

    public Annonce Add(Annonce annonce)
    {
        _collection.InsertOne(annonce);
        return annonce;
    }

    public Annonce Update(Annonce annonce)
    {
        _collection.ReplaceOne(annonce.Id, annonce);
        return annonce;
    }

    public void Delete(string id)
    {
        _collection.DeleteOne(id);
    }
}
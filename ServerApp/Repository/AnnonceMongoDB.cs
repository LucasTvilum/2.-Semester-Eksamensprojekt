using MongoDB.Driver;
using Core.Models;
using ServerApp.Repository;


public class AnnonceMongoDB : IAnnonceRepository
{
    private readonly IMongoCollection<Annonce> _collection;

    public AnnonceMongoDB()
    {
        var client = new MongoClient("mongodb://localhost:27017");
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
        var filter = Builders<Annonce>.Filter.Eq(a => a.Id, annonce.Id);
        _collection.ReplaceOne(filter, annonce);
        return annonce;
    }

    public void Delete(string id)
    {
        _collection.DeleteOne(id);
    }

    public List<Annonce> GetFiltered(Annonce filter)
    {
        var query = _collection.AsQueryable();

        if (!string.IsNullOrEmpty(filter.Type))
            query = query.Where(a => a.Type == filter.Type);

        if (filter.Price > 0)
            query = query.Where(a => a.Price == filter.Price);

        if (!string.IsNullOrEmpty(filter.Color))
            query = query.Where(a => a.Color == filter.Color);
        
        if (!string.IsNullOrEmpty(filter.lokale.Name))
            query = query.Where(a => a.lokale == filter.lokale);
        

        return query.ToList();
    }
}
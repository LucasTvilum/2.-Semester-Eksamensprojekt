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
        _collection.ReplaceOne(annonce.Id, annonce);
        return annonce;
    }

    public void Delete(string id)
    {
        _collection.DeleteOne(id);
    }

    public List<Annonce> GetFiltered(AnnonceFilter filter)
    {
        var builder = Builders<Annonce>.Filter;
        var filters = new List<FilterDefinition<Annonce>>();

        if (!string.IsNullOrWhiteSpace(filter.Type))
            filters.Add(builder.Eq(a => a.Type, filter.Type));

        if (!string.IsNullOrWhiteSpace(filter.Size))
            filters.Add(builder.Eq(a => a.Size, filter.Size));

        if (filter.Price > 0)
            filters.Add(builder.Lte(a => a.Price, filter.Price));

        if (!string.IsNullOrWhiteSpace(filter.Color))
            filters.Add(builder.Eq(a => a.Color, filter.Color));

        if (!string.IsNullOrWhiteSpace(filter.lokale.Name))
            filters.Add(builder.Eq(a => a.lokale.Name, filter.lokale.Name));

        if (!filters.Any())
            return _collection.Find(_ => true).ToList();

        return _collection.Find(builder.And(filters)).ToList();
    }

    }

    

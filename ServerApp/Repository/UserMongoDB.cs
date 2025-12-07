using MongoDB.Driver;
using Core.Models;
using ServerApp.Repository;

namespace ServerApp.Repository;
public class UserMongoDB : IUserRepository
{
    private readonly IMongoCollection<User> _collection;
    //
    private readonly IMongoDatabase _db;
    //for inheritance
    public UserMongoDB()
    {
        var client = new MongoClient("mongodb://localhost:27017");
        _db = client.GetDatabase("Vinduespudsning");
        _collection = _db.GetCollection<User>("Users");
    }

    public List<User> GetAll()
    {
        return _collection.Find(_ => true).ToList();
    }

    public void Add(User user)
    {
        _collection.InsertOne(user);
    }

    public User Update(User user)
    {
        var filter = Builders<User>.Filter.Eq(a => a.Id, user.Id);
        _collection.ReplaceOne(filter, user);
        return user;
    }

    public void Delete(string id)
    {
        _collection.DeleteOne(id);
    }
    public Task<List<Customer>> GetCustomers()
    {
        var col = _db.GetCollection<Customer>("Users");
        return col.Find(_ => true).ToListAsync();
    }

    public Task<List<Worker>> GetWorkers()
    {
        var col = _db.GetCollection<Worker>("Users");
        return col.Find(_ => true).ToListAsync();
    }
}
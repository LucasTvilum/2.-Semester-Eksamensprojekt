using MongoDB.Driver;
using Core.Models;
using ServerApp.Repository;

namespace ServerApp.Repository;
public class UserMongoDB : IUserRepository
{
    private readonly IMongoCollection<User> _collection;

    public UserMongoDB()
    {
        var client = new MongoClient("mongodb://localhost:27017");
        var database = client.GetDatabase("Vinduespudsning");
        _collection = database.GetCollection<User>("Users");
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

}
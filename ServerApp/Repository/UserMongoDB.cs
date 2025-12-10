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
        //Mere inheritance relateret, checker om RUNTIME typen på user er User eller Customer
        if (user is Customer customer)
        {
            Console.WriteLine("Customer Address: " + customer.Address);
            Console.WriteLine("Customer Region: " + customer.Region);
            Console.WriteLine("Customer City: " + customer.City);
        }
        else if (user is Worker worker)
        {
            Console.WriteLine("Worker object received");
        }
        
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
        var filter = Builders<User>.Filter.Eq(u => u.Id, id);
        _collection.DeleteOne(filter);
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
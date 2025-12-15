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
    public UserMongoDB(DatabaseSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        _db = client.GetDatabase(settings.DatabaseName);
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
            Console.WriteLine("Customer Id: " +  customer.Id);
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
    public async Task<List<Customer>> GetCustomers()
    {
        var col = _db.GetCollection<Customer>("Users");

        var filter = Builders<Customer>.Filter.AnyEq("_t", "Customer"); // _t contains "Worker"
        var customers = await col.Find(filter).ToListAsync();


        foreach (var customer in customers)
        {
            Console.WriteLine($"CustomerId: {customer.Id}");
        }
    
        return customers;
    }

    public async Task<List<Worker>> GetWorkers()
    {
        var col = _db.GetCollection<Worker>("Users");

        var filter = Builders<Worker>.Filter.AnyEq("_t", "Worker"); // _t contains "Worker"
        var workers = await col.Find(filter).ToListAsync();

        foreach (var worker in workers)
        {
            Console.WriteLine($"WorkerId: {worker.Id}");
        }

        return workers;
    }
}
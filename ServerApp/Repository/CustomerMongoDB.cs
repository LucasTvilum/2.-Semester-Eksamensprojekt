using MongoDB.Driver;
using Core.Models;
using ServerApp.Repository;


public class CustomerMongoDB : ICustomerInterface
{
    private readonly IMongoCollection<Customer> _collection;

    public CustomerMongoDB()
    {
        var client = new MongoClient("mongodb://localhost:27017");
        var database = client.GetDatabase("Vinduespudsning");
        _collection = database.GetCollection<Customer>("customers");
    }

    public List<Customer> GetAll()
    {
        return _collection.Find(_ => true).ToList();
    }

    public void Add(Customer customer)
    {
        _collection.InsertOne(customer);
    }

    public Customer Update(Customer customer)
    {
        var filter = Builders<Customer>.Filter.Eq(a => a.UserId, customer.UserId);
        _collection.ReplaceOne(filter, customer);
        return customer;
    }

    public void Delete(string id)
    {
        _collection.DeleteOne(id);
    }

}
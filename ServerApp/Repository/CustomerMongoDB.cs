using MongoDB.Driver;
using Core.Models;
using ServerApp.Repository;

namespace ServerApp.Repository;
public class CustomerMongoDB : ICustomerRepository
{
    private readonly IMongoCollection<Customer> _collection;

    public CustomerMongoDB()
    {
        var client = new MongoClient("mongodb://localhost:27017");
        var database = client.GetDatabase("Vinduespudsning");
        _collection = database.GetCollection<Customer>("Customers");
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
        var filter = Builders<Customer>.Filter.Eq(a => a.Id, customer.Id);
        _collection.ReplaceOne(filter, customer);
        return customer;
    }

    public void Delete(string id)
    {
        _collection.DeleteOne(id);
    }

}
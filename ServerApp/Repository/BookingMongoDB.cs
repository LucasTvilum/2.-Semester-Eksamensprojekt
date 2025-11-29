using MongoDB.Driver;
using Core.Models;
using ServerApp.Repository;


public class BookingMongoDB : IBookingInterface
{
    private readonly IMongoCollection<Booking> _collection;

    public BookingMongoDB()
    {
        var client = new MongoClient("mongodb://localhost:27017");
        var database = client.GetDatabase("Vinduespudsning");
        _collection = database.GetCollection<Booking>("Bookings");
    }

    public List<Booking> GetAll()
    {
        return _collection.Find(_ => true).ToList();
    }

    public void Add(Booking booking)
    {
        _collection.InsertOne(booking);
    }

    public Booking Update(Booking booking)
    {
        var filter = Builders<Booking>.Filter.Eq(a => a.BookingId, booking.BookingId);
        _collection.ReplaceOne(filter, booking);
        return booking;
    }

    public void Delete(string id)
    {
        _collection.DeleteOne(id);
    }

}
using MongoDB.Driver;
using Core.Models;
using MongoDB.Bson.Serialization;
using ServerApp.Repository;

namespace ServerApp.Repository;
public class BookingMongoDB : IBookingRepository
{
    private readonly IMongoCollection<Booking> _collection;
    
   public BookingMongoDB(DatabaseSettings settings)
   {
       var client = new MongoClient(settings.ConnectionString);
       var database = client.GetDatabase(settings.DatabaseName);
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
        var filter = Builders<Booking>.Filter.Eq(a => a.Id, booking.Id);
        _collection.ReplaceOne(filter, booking);
        return booking;
    }

    public void Delete(string id)
    {
        _collection.DeleteOne(id);
    }

}
using MongoDB.Driver;
using Core.Models;
using MongoDB.Bson.Serialization;
using ServerApp.Repository;

namespace ServerApp.Repository;
public class WorkTaskMongoDB : IWorkTaskRepository
{
    private readonly IMongoCollection<WorkTask> _collection;

    public WorkTaskMongoDB()
    {
        var client = new MongoClient("mongodb://localhost:27017");
        var database = client.GetDatabase("Vinduespudsning");
        _collection = database.GetCollection<WorkTask>("WorkTasks");
    }

    public List<WorkTask> GetAll()
    {
        return _collection.Find(_ => true).ToList();
    }

    public void Add(WorkTask worktask)
    {
        _collection.InsertOne(worktask);
    }

    public WorkTask Update(WorkTask worktask)
    {
        var filter = Builders<WorkTask>.Filter.Eq(a => a.Id, worktask.Id);
        _collection.ReplaceOne(filter, worktask);
        return worktask;
    }

    public void Delete(string id)
    {
        _collection.DeleteOne(id);
    }

}
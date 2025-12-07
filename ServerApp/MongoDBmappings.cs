using Core.Models;
using MongoDB.Bson.Serialization;

namespace ServerApp


// ChatGPT generet kode. Det er her for at sikre at inheritance for user, worker, customer virker korrekt med MongoDB samtidigt med at modelklasserne er clean
// Se her for mere info: https://www.mongodb.com/docs/drivers/csharp/current/serialization/polymorphic-objects/
{
    public static class MongoDBmappings
    {
        public static void Register()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(User)))
            {
                BsonClassMap.RegisterClassMap<User>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIsRootClass(true);
                    cm.MapMember(u => u.Id)
                        .SetElementName("_id");
                    cm.MapMember(u => u.Usertype)
                        .SetElementName("userType");
                });
            }

            if (!BsonClassMap.IsClassMapRegistered(typeof(Customer)))
            {
                BsonClassMap.RegisterClassMap<Customer>(cm =>
                {
                    cm.AutoMap();
                    cm.SetDiscriminator("Customer");
                });
            }

            if (!BsonClassMap.IsClassMapRegistered(typeof(Worker)))
            {
                BsonClassMap.RegisterClassMap<Worker>(cm =>
                {
                    cm.AutoMap();
                    cm.SetDiscriminator("Worker");
                });
            }
        }
    }
}
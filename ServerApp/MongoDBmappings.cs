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
            // Base User class
            if (!BsonClassMap.IsClassMapRegistered(typeof(User)))
            {
                BsonClassMap.RegisterClassMap<User>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIsRootClass(true);
                    cm.MapIdMember(u => u.Id).SetElementName("_id");
                    cm.MapMember(u => u.Usertype).SetElementName("Usertype");
                    cm.MapMember(u => u.Username);
                    cm.MapMember(u => u.Password);
                    cm.MapMember(u => u.Name);
                    cm.MapMember(u => u.Mail);
                    cm.MapMember(u => u.PhoneNumber);
                });
            }

            // Customer class with extra fields
            if (!BsonClassMap.IsClassMapRegistered(typeof(Customer)))
            {
                BsonClassMap.RegisterClassMap<Customer>(cm =>
                {
                    cm.AutoMap();
                    cm.SetDiscriminator("Customer");
                    cm.MapMember(c => c.Address);
                    cm.MapMember(c => c.Region);
                    cm.MapMember(c => c.City);
                    cm.MapMember(c => c.Subscription); // any other extra customer fields
                    cm.SetIgnoreExtraElements(true); // avoids FormatException for unknown fields
                });
            }

            // Worker class with extra fields
            if (!BsonClassMap.IsClassMapRegistered(typeof(Worker)))
            {
                BsonClassMap.RegisterClassMap<Worker>(cm =>
                {
                    cm.AutoMap();
                    cm.SetDiscriminator("Worker");
                    cm.MapMember(w => w.Admin); // map Worker-specific bool
                    cm.SetIgnoreExtraElements(true); // in case other fields exist in BSON
                });
            }
        }
    }
}
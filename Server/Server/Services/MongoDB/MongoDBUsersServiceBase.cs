using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Server.Services.Abstract;
using Server.Config;
using Common;

namespace Server.Services.MongoDB
{
    public class MongoDBUsersServiceBase<T> : IUsersService<T>
        where T : IPerson
    {
        protected readonly IMongoCollection<T> _collection;

        public MongoDBUsersServiceBase(IDBSettings settings, string collectionName)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _collection = database.GetCollection<T>(settings.CollectionsNames[collectionName]);
        }

        public T SignIn(string username, string password)
        {
            return _collection.Find(doctor => doctor.Username == username
                                           && doctor.Password == password).FirstOrDefault();
        }

        public bool Register(T person)
        {
            try
            {
                if (IsExist(person.Username))
                {
                    return false;
                }

                _collection.InsertOne(person);
                return true;
            }
            catch (MongoWriteException)
            {
                return false;
            }
        }

        public bool ScheduleVisit(T person, Visit visit)
        {
            try
            {
                var update = Builders<T>.Update.Push(per => per.VisitsId, visit.Id);
                Update(person.Username, update);
                return true;
            }
            catch (MongoWriteException)
            {
                return false;
            }
        }

        public T Get(string username)
        {
            return _collection.Find(per => per.Username == username).FirstOrDefault();
        }

        public List<Visit> GetVisits(string username)
        {
            var visits = _collection.Aggregate().Match(per => per.Username == username)
                .Lookup("Visits", "VisitsId", "_id", "Visits")
                .Project(p => new { Username = p["_id"], Visits = p["Visits"] })
                .FirstOrDefault();

            return BsonSerializer.Deserialize<PersonVisits>(visits.ToBsonDocument())
                .Visits.OrderBy(visit => visit.StartTime).Take(5).ToList();
        }

        protected void Update(string username, UpdateDefinition<T> update)
        {
            _collection.UpdateOne(per => per.Username == username, update);
        }

        protected bool AreVisitsOverlapping(Visit visit1, Visit visit2)
        {
            return (visit1.StartTime > visit2.StartTime && visit1.StartTime < visit2.EndTime) ||
                   (visit2.StartTime > visit1.StartTime && visit2.StartTime < visit1.EndTime);
        }

        protected bool IsExist(string username)
        {
            return _collection.Find(per => per.Username == username).CountDocuments() >= 1;
        }
    }
}

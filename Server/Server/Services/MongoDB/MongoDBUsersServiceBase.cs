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
    public abstract class MongoDBUsersServiceBase<T> : IUsersService<T>
        where T : IPerson
    {
        protected readonly IMongoCollection<T> _collection;

        public MongoDBUsersServiceBase(MongoClient client, IDBSettings settings, string collectionName)
        {
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

        public List<Visit> GetVisits(string username)
        {
            var visits = _collection.Aggregate().Match(per => per.Username == username)
                .Lookup(LookupFields.FOREIGN_COLLECTION_NAME, LookupFields.LOCAL_FIELD,
                        LookupFields.FOREIGN_FIELD, LookupFields.AS)
                .Project(p => new { Username = p[LookupFields.FOREIGN_FIELD], Visits = p[LookupFields.AS] })
                .FirstOrDefault();

            return BsonSerializer.Deserialize<PersonVisits>(visits.ToBsonDocument())
                .Visits.OrderBy(visit => visit.StartTime).Take(5).ToList();
        }

        public T Get(string username)
        {
            return _collection.Find(per => per.Username == username).FirstOrDefault();
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

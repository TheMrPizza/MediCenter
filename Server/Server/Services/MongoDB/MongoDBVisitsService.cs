using MongoDB.Driver;
using Server.Services.Abstract;
using Server.Config;
using Common;

namespace Server.Services.MongoDB
{
    public class MongoDBVisitsService : IVisitsService
    {
        private readonly IMongoCollection<Visit> _visits;

        public MongoDBVisitsService(IDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _visits = database.GetCollection<Visit>(settings.CollectionsNames["Visits"]);
        }

        public Visit GetVisit(string id)
        {
            return _visits.Find(visit => visit.Id == id).FirstOrDefault();
        }

        public bool Schedule(Visit visit)
        {
            try
            {
                _visits.InsertOne(visit);
                return true;
            }
            catch (MongoWriteException)
            {
                return false;
            }
        }
    }
}

using System.Linq;
using MongoDB.Driver;
using Server.Services.Abstract;
using Server.Config;
using Common;

namespace Server.Services.MongoDB
{
    public class MongoDBPatientsService : IPatientsService
    {
        private readonly IMongoCollection<Patient> _patients;

        public MongoDBPatientsService(IDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _patients = database.GetCollection<Patient>(settings.CollectionsNames["Patients"]);
        }

        public Patient SignIn(string username, string password)
        {
            return _patients.Find(patient => patient.Username == username
                                  && patient.Password == password).FirstOrDefault();
        }

        public bool Register(Patient patient)
        {
            try
            {
                _patients.InsertOne(patient);
                return true;
            }
            catch (MongoWriteException)
            {
                return false;
            }
        }

        public bool ScheduleVisit(Patient patient, Visit visit)
        {
            try
            {
                var update = Builders<Patient>.Update.Push(patient => patient.VisitsId, visit.Id);
                Update(patient.Username, update);
                return true;
            }
            catch (MongoWriteException)
            {
                return false;
            }
        }

        public Patient Get(string username)
        {
            return _patients.Find(patient => patient.Username == username).FirstOrDefault();
        }

        public void Update(string username, UpdateDefinition<Patient> update)
        {
            _patients.UpdateOne(patient => patient.Username == username, update);
        }
    }
}

using System.Linq;
using MongoDB.Driver;
using Server.Config;
using Common;

namespace Server.Services
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
    }
}

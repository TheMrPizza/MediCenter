using System.Linq;
using MongoDB.Driver;
using Server.Config;
using Common;

namespace Server.Services
{
    public class MongoDBDoctorsService : IDoctorsService
    {
        private readonly IMongoCollection<Doctor> _doctors;

        public MongoDBDoctorsService(IDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _doctors = database.GetCollection<Doctor>(settings.CollectionsNames["Doctors"]);
        }

        public Doctor SignIn(string username, string password)
        {
            return _doctors.Find(doctor => doctor.Username == username
                                 && doctor.Password == password).FirstOrDefault();
        }

        public bool Register(Doctor doctor)
        {
            try
            {
                _doctors.InsertOne(doctor);
                return true;
            }
            catch (MongoWriteException)
            {
                return false;
            }
        }

        public Doctor FindDoctorForVisit(Visit visit)
        {
            return _doctors.Find(
                doc => doc.Specialities.Contains(visit.Speciality) &&
                       doc.Visits.All(vis => !AreVisitsOverlapping(visit, vis)))
                .FirstOrDefault();
        }

        private bool AreVisitsOverlapping(Visit visit1, Visit visit2)
        {
            return (visit1.StartTime > visit2.StartTime && visit1.StartTime < visit2.EndTime)
                || (visit2.StartTime > visit1.StartTime && visit2.StartTime < visit1.EndTime);
        }
    }
}

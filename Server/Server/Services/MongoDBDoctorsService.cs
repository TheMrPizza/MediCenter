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

        /// <summary>
        /// Returns a doctor who has the requested speciality, and his visits don't overlapp
        /// the requested visit times
        /// </summary>
        public Doctor FindDoctorForVisit(Visit visit)
        {

            return _doctors.AsQueryable().FirstOrDefault(doc =>
                doc.Specialities.Contains(visit.Speciality) &&
                !doc.Visits.Any(vis =>
                    (vis.StartTime > visit.StartTime && vis.StartTime < visit.EndTime) ||
                    (visit.StartTime > vis.StartTime && visit.StartTime < vis.EndTime)
                )
            );
        }
    }
}

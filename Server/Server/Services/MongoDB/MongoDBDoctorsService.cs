using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Server.Services.Abstract;
using Server.Config;
using Common;

namespace Server.Services.MongoDB
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
            var a = _doctors.Aggregate().Match(doc => doc.Specialities.Contains(visit.Speciality))
                .Lookup("Visits", "VisitsId", "Id", "Visits")
                .ToList();

            DoctorVisits dv = a.Select(document => BsonSerializer.Deserialize<DoctorVisits>(document))
                .FirstOrDefault(dv => !dv.Visits.Any(vis => AreVisitsOverlapping(vis, visit)));

            return dv;
        }

        public bool AreVisitsOverlapping(Visit visit1, Visit visit2)
        {
            return (visit1.StartTime > visit2.StartTime && visit1.StartTime < visit2.EndTime) ||
                   (visit2.StartTime > visit1.StartTime && visit2.StartTime < visit1.EndTime);
        }

        public class DoctorVisits : Doctor
        {
            public List<Visit> Visits { get; set; }
        }
    }
}

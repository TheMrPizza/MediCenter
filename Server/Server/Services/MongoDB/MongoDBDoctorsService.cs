using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
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
            var doctorVisits = _doctors.Aggregate().Match(doc => doc.Specialities.Contains(visit.Speciality))
                .Lookup("Visits", "VisitsId", "_id", "Visits")
                .Project(p => new { Username = p["_id"], Visits = p["Visits"] })
                .ToList();

            DoctorVisits dv = doctorVisits.Select(document => BsonSerializer.Deserialize<DoctorVisits>(document.ToBsonDocument()))
                .FirstOrDefault(dv => !dv.Visits.Any(vis => AreVisitsOverlapping(vis, visit)));

            if (dv == null)
            {
                return null;
            }

            return Get(dv.Username);
        }

        public bool ScheduleVisit(Doctor doctor, Visit visit)
        {
            try
            {
                var update = Builders<Doctor>.Update.Push(doc => doc.VisitsId, visit.Id);
                Update(doctor.Username, update);
                return true;
            }
            catch (MongoWriteException)
            {
                return false;
            }
        }

        public List<Visit> GetDoctorVisits(string username)
        {
            var visits = _doctors.Aggregate().Match(doc => doc.Username == username)
                .Lookup("Visits", "VisitsId", "_id", "Visits")
                .Project(p => new { Visits = p["Visits"] })
                .ToList();

            return visits.Select(document => BsonSerializer.Deserialize<Visit>(document.ToBsonDocument()))
                    .Take(5).ToList();
        }

        public Doctor Get(string username)
        {
            return _doctors.Find(doc => doc.Username == username).FirstOrDefault();
        }

        public void Update(string username, UpdateDefinition<Doctor> update)
        {
            _doctors.UpdateOne(doc => doc.Username == username, update);
        }

        private bool AreVisitsOverlapping(Visit visit1, Visit visit2)
        {
            return (visit1.StartTime > visit2.StartTime && visit1.StartTime < visit2.EndTime) ||
                   (visit2.StartTime > visit1.StartTime && visit2.StartTime < visit1.EndTime);
        }
    }
}

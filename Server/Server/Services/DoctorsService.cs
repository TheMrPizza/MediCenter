using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Server.Models;
using Common;

namespace Server.Services
{
    public class DoctorsService
    {
        private readonly IMongoCollection<Doctor> _doctors;

        public DoctorsService(IMongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _doctors = database.GetCollection<Doctor>(settings.DoctorsCollectionName);
        }

        public List<Doctor> GetAll()
        {
            return _doctors.Find(doctor => true).ToList();
        }

        public Doctor Get(string id)
        {
            return _doctors.Find(doctor => doctor.Id == id).FirstOrDefault();
        }

        public Doctor Add(Doctor doctor)
        {
            _doctors.InsertOne(doctor);
            return doctor;
        }

        public void Update(string id, Doctor doctor)
        {
            _doctors.ReplaceOne(doc => doc.Id == id, doctor);
        }

        public void Remove(string id)
        {
            _doctors.DeleteOne(doctor => doctor.Id == id);
        }

        public void Remove(Doctor doctor)
        {
            _doctors.DeleteOne(doc => doc.Id == doctor.Id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Config;
using MongoDB.Driver;
using Common;

namespace Server.Services
{
    public class UsersService
    {
        private readonly IMongoCollection<Doctor> _doctors;
        private readonly IMongoCollection<Patient> _patients;

        public UsersService(IMongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _doctors = database.GetCollection<Doctor>(settings.CollectionsNames["Doctors"]);
            _patients = database.GetCollection<Patient>(settings.CollectionsNames["Patients"]);
        }

        //public bool RegisterDoctor(Doctor doctor)
        //{
        //    if (_doctors.Find(doc => doc.GetId() == doctor.GetId()).CountDocuments() == 0)
        //    {
        //        _doctors.InsertOne(doctor);
        //        return true;
        //    }

        //    return false;
        //}

        public bool RegisterDoctor(Doctor doctor)
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

        public bool RegisterPatient(Patient patient)
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

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

        public Doctor SignInDoctor(string username, string password)
        {
            return _doctors.Find(doctor => doctor.Username == username
                                 && doctor.Password == password).FirstOrDefault();
        }

        public Patient SignInPatient(string username, string password)
        {
            return _patients.Find(patient => patient.Username == username
                                  && patient.Password == password).FirstOrDefault();
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

﻿using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
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

        public bool CheckNewVisit(Visit visit)
        {
            if (visit.StartTime > DateTime.Now)
            {
                List<Visit> allVisits = GetVisits(visit.PatientUsername);
                return !allVisits.Any(vis => AreVisitsOverlapping(vis, visit));
            }

            return false;
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

        public List<Visit> GetVisits(string username)
        {
            var visits = _patients.Aggregate().Match(doc => doc.Username == username)
                .Lookup("Visits", "VisitsId", "_id", "Visits")
                .Project(p => new { Username = p["_id"], Visits = p["Visits"] })
                .FirstOrDefault();

            return BsonSerializer.Deserialize<PersonVisits>(visits.ToBsonDocument())
                .Visits.OrderBy(visit => visit.StartTime).Take(5).ToList();
        }

        public Patient Get(string username)
        {
            return _patients.Find(patient => patient.Username == username).FirstOrDefault();
        }

        public void Update(string username, UpdateDefinition<Patient> update)
        {
            _patients.UpdateOne(patient => patient.Username == username, update);
        }

        private bool AreVisitsOverlapping(Visit visit1, Visit visit2)
        {
            return (visit1.StartTime > visit2.StartTime && visit1.StartTime < visit2.EndTime) ||
                   (visit2.StartTime > visit1.StartTime && visit2.StartTime < visit1.EndTime);
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using Server.Services.Abstract;
using Server.Config;
using Common;

namespace Server.Services.MongoDB
{
    public class MongoDBVisitsService : IVisitsService
    {
        private readonly IMongoCollection<Visit> _visits;

        public MongoDBVisitsService(MongoClient client, IDBSettings settings)
        {
            var database = client.GetDatabase(settings.DatabaseName);
            _visits = database.GetCollection<Visit>(settings.CollectionsNames["Visits"]);
        }

        public bool Schedule(Visit visit)
        {
            try
            {
                _visits.InsertOne(visit);
                return true;
            }
            catch (MongoWriteException)
            {
                return false;
            }
        }

        public Visit AddPrescriptions(Visit visit, Patient patient, List<Medicine> medicines)
        {
            List<string> safetyMedicines = medicines.Where(med => CheckMedicineForPatience(patient, med))
                .Select(med => med.Id).ToList();

            try
            {
                visit.MedicinesId = safetyMedicines;
                var update = Builders<Visit>.Update.Set(vis => vis.MedicinesId, safetyMedicines);
                Update(visit.Id, update);
                return visit;
            }
            catch (MongoWriteException)
            {
                return null;
            }
        }

        public Visit Get(string id)
        {
            return _visits.Find(visit => visit.Id == id).FirstOrDefault();
        }

        private void Update(string id, UpdateDefinition<Visit> update)
        {
            _visits.UpdateOne(visit => visit.Id == id, update);
        }

        private bool CheckMedicineForPatience(Patient patient, Medicine medicine)
        {
            if (patient == null)
            {
                return false;
            }

            return !medicine.Reactions.Any(disease => patient.Diseases.Contains(disease));
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using Server.Services.Abstract;
using Server.Config;
using Common;

namespace Server.Services.MongoDB
{
    public class MongoDBMedicinesService : IMedicinesService
    {
        private readonly IMongoCollection<Medicine> _medicines;

        public MongoDBMedicinesService(MongoClient client, IDBSettings settings)
        {
            var database = client.GetDatabase(settings.DatabaseName);
            _medicines = database.GetCollection<Medicine>(settings.CollectionsNames["Medicines"]);
        }

        public List<Medicine> GetAll()
        {
            return _medicines.Find(med => true).ToList();
        }
    }
}

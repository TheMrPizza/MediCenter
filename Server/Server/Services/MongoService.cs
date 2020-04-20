using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Models;
using MongoDB.Driver;
using Common;

namespace Server.Services
{
    public class MongoService<TModel> : IService<TModel>
        where TModel : IModel
    {
        protected readonly IMongoCollection<TModel> _collection;

        public MongoService(IMongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _collection = database.GetCollection<TModel>(settings.DoctorsCollectionName);
        }

        public List<TModel> GetAll()
        {
            return _collection.Find(document => true).ToList();
        }

        public TModel Get(string id)
        {
            return _collection.Find(document => document.Id == id).FirstOrDefault();
        }

        public TModel Add(TModel model)
        {
            _collection.InsertOne(model);
            return model;
        }

        public void Update(string id, TModel model)
        {
            _collection.ReplaceOne(document => document.Id == id, model);
        }

        public void Remove(string id)
        {
            _collection.DeleteOne(document => document.Id == id);
        }

        public void Remove(TModel model)
        {
            _collection.DeleteOne(document => document.Id == model.Id);
        }
    }
}

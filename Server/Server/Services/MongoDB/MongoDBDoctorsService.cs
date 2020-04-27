﻿using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Server.Services.Abstract;
using Server.Config;
using Common;

namespace Server.Services.MongoDB
{
    public class MongoDBDoctorsService : MongoDBUsersServiceBase<Doctor>, IDoctorsService
    {
        public MongoDBDoctorsService(MongoClient client, IDBSettings settings) : base(client, settings, "Doctors")
        {

        }

        /// <summary>
        /// Returns a doctor who has the requested speciality, and his visits don't overlapp
        /// the requested visit times
        /// </summary>
        public Doctor FindDoctorForVisit(Visit visit)
        {
            var visits = _collection.Aggregate().Match(doc => doc.Specialities.Contains(visit.Speciality))
                .Lookup(LookupFields.FOREIGN_COLLECTION_NAME, LookupFields.LOCAL_FIELD,
                        LookupFields.FOREIGN_FIELD, LookupFields.AS)
                .Project(p => new { Username = p[LookupFields.FOREIGN_FIELD], Visits = p[LookupFields.AS] })
                .ToList();

            PersonVisits dv = visits.Select(document => BsonSerializer.Deserialize<PersonVisits>(document.ToBsonDocument()))
                .FirstOrDefault(dv => !dv.Visits.Any(vis => AreVisitsOverlapping(vis, visit)));

            if (dv == null)
            {
                return null;
            }

            return Get(dv.Username);
        }
    }
}

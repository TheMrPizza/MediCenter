using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using Server.Services.Abstract;
using Server.Config;
using Common;

namespace Server.Services.MongoDB
{
    public class MongoDBPatientsService : MongoDBUsersServiceBase<Patient>, IPatientsService
    {
        public MongoDBPatientsService(MongoClient client, IDBSettings settings) : base(client, settings, "Patients")
        {

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
    }
}

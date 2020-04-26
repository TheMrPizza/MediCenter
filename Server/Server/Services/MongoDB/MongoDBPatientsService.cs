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
        public MongoDBPatientsService(IDBSettings settings) : base(settings, "Patients")
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

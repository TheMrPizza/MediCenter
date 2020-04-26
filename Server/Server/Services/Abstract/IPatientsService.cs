using System.Collections.Generic;
using Common;

namespace Server.Services.Abstract
{
    public interface IPatientsService
    {
        public Patient SignIn(string username, string password);
        public bool Register(Patient doctor);
        bool ScheduleVisit(Patient patient, Visit visit);
        List<Visit> GetVisits(string username);
        Patient Get(string username);
    }
}

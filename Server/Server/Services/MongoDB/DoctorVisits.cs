using System.Collections.Generic;
using Common;

namespace Server.Services.MongoDB
{
    public class DoctorVisits
    {
        public string Username { get; set; }
        public List<Visit> Visits { get; set; }
    }
}

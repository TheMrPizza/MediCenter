using Server.Config;

namespace Server.Services
{
    public class MongoDBService : IDBService
    {
        public IDoctorsService DoctorsService { get; }
        public IPatientsService PatientsService { get; }

        public MongoDBService(IDBSettings settings)
        {
            DoctorsService = new MongoDBDoctorsService(settings);
            PatientsService = new MongoDBPatientsService(settings);
        }
    }
}

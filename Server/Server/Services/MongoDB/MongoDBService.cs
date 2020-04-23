using Server.Services.Abstract;
using Server.Config;

namespace Server.Services.MongoDB
{
    public class MongoDBService : IDBService
    {
        public IDoctorsService DoctorsService { get; }
        public IPatientsService PatientsService { get; }
        public IVisitsService VisitsService { get; }

        public MongoDBService(IDBSettings settings)
        {
            DoctorsService = new MongoDBDoctorsService(settings);
            PatientsService = new MongoDBPatientsService(settings);
            VisitsService = new MongoDBVisitsService(settings);
        }
    }
}

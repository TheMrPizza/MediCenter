using Server.Services.Abstract;

namespace Server.Services.MongoDB
{
    public class MongoDBService : IDBService
    {
        public IDoctorsService DoctorsService { get; }
        public IPatientsService PatientsService { get; }
        public IVisitsService VisitsService { get; }

        public MongoDBService(IDoctorsService doctorsService, IPatientsService patientsService,
            IVisitsService visitsService)
        {
            DoctorsService = doctorsService;
            PatientsService = patientsService;
            VisitsService = visitsService;
        }
    }
}

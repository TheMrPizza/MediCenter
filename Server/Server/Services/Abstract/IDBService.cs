namespace Server.Services.Abstract
{
    public interface IDBService
    {
        IDoctorsService DoctorsService { get; }
        IPatientsService PatientsService { get; }
        IVisitsService VisitsService { get; }
    }
}

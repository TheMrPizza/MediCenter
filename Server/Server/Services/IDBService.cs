namespace Server.Services
{
    public interface IDBService
    {
        IDoctorsService DoctorsService { get; }
        IPatientsService PatientsService { get; }
    }
}

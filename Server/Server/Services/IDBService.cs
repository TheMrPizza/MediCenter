using Common;

namespace Server.Services
{
    public interface IDBService
    {
        Doctor SignInDoctor(string username, string password);
        Patient SignInPatient(string username, string password);
        bool Register(Doctor doctor);
        bool Register(Patient patient);
    }
}

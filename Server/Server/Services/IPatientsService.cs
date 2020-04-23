using Common;

namespace Server.Services
{
    public interface IPatientsService
    {
        public Patient SignIn(string username, string password);
        public bool Register(Patient doctor);
    }
}

using Common;

namespace Server.Services.Abstract
{
    public interface IPatientsService
    {
        public Patient SignIn(string username, string password);
        public bool Register(Patient doctor);
    }
}

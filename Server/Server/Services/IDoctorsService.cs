using Common;

namespace Server.Services
{
    public interface IDoctorsService
    {
        Doctor SignIn(string username, string password);
        bool Register(Doctor doctor);
    }
}

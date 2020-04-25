using Common;

namespace Server.Services.Abstract
{
    public interface IDoctorsService
    {
        Doctor SignIn(string username, string password);
        bool Register(Doctor doctor);
        Doctor FindDoctorForVisit(Visit visit);
        bool ScheduleVisit(Doctor doctor, Visit visit);
        Doctor Get(string username);
    }
}

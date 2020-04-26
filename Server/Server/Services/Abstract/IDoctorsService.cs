using Common;

namespace Server.Services.Abstract
{
    public interface IDoctorsService : IUsersService<Doctor>
    {
        Doctor FindDoctorForVisit(Visit visit);
    }
}

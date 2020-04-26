using Common;

namespace Server.Services.Abstract
{
    public interface IPatientsService : IUsersService<Patient>
    {
        bool CheckNewVisit(Visit visit);
    }
}

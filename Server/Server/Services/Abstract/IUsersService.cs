using System.Collections.Generic;
using Common;

namespace Server.Services.Abstract
{
    public interface IUsersService<T>
        where T: IPerson
    {
        T SignIn(string username, string password);
        bool Register(T person);
        bool ScheduleVisit(T person, Visit visit);
        List<Visit> GetVisits(string username);
        T Get(string username);
    }
}

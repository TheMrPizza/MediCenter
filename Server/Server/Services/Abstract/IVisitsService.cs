using Common;

namespace Server.Services.Abstract
{
    public interface IVisitsService
    {
        Visit GetVisit(string id);
        bool Schedule(Visit visit);
    }
}

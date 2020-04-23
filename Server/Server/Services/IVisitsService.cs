using Common;

namespace Server.Services
{
    public interface IVisitsService
    {
        bool Schedule(Visit visit);
    }
}

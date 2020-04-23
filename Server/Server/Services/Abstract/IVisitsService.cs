using Common;

namespace Server.Services.Abstract
{
    public interface IVisitsService
    {
        bool Schedule(Visit visit);
    }
}

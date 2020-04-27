using Client.Exceptions;

namespace Client.IO.Abstract
{
    public abstract class ErrorElementBase
    {
        public abstract void Interact(MediCenterException exception);
    }
}

using Client.HttpClients;
using Client.IO.Abstract;

namespace Client.Actions.IOManagers
{
    public abstract class IOManagerBase<T>
    {
        protected MediClient _client { get; }
        protected IStreamIO _streamIO { get; }

        public IOManagerBase(MediClient client, IStreamIO streamIO)
        {
            _client = client;
            _streamIO = streamIO;
        }

        public virtual void PrintInstructions()
        {

        }

        public virtual T GetInput()
        {
            return ProcessInput();
        }

        public virtual T ProcessInput()
        {
            return default;
        }
    }
}

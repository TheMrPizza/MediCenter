using Client.HttpClients;
using Client.IO.Abstract;

namespace Client.Actions.InputManagers
{
    public abstract class InputManagerBase<T>
    {
        protected MediClient _client { get; }
        protected IStreamIO _streamIO { get; }

        public InputManagerBase(MediClient client, IStreamIO streamIO)
        {
            _client = client;
            _streamIO = streamIO;
        }

        public virtual void Init(InputParams inputParams)
        {

        }

        public virtual void PrintInstructions()
        {

        }

        public abstract T GetInput();
    }
}

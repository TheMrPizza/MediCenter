using Client.HttpClients;
using Client.IO.Abstract;

namespace Client.Actions.OutputManagers
{
    public interface IOutputManager<T>
    {
        public MediClient Client { get; }
        public IStreamIO StreamIO { get; }

        public T Value { get; set; }

        public void PrintOutput(T value);
    }
}

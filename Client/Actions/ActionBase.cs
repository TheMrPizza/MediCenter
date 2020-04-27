using System;
using System.Threading.Tasks;
using Client.HttpClients;
using Client.IO.Abstract;

namespace Client.Actions
{
    public abstract class ActionBase
    {
        protected MediClient _client { get; }
        protected IStreamIO _streamIO { get; }

        public event Action<ActionBase> OnMainMenuAction;

        public ActionBase(MediClient client, IStreamIO streamIO)
        {
            _client = client;
            _streamIO = streamIO;
        }

        protected void SetMainMenuAction(ActionBase mainMenuAction)
        {
            OnMainMenuAction?.Invoke(mainMenuAction);
        }

        public abstract Task<ActionBase> Run();
    }
}

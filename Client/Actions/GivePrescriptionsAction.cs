using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Actions.InputManagers;
using Client.HttpClients;
using Client.IO.Abstract;
using Client.Exceptions;
using Common;

namespace Client.Actions
{
    public class GivePrescriptionsAction : ActionBase
    {
        public InputManagerBase<SignInContent> InputManager { get; set; }

        public GivePrescriptionsAction(MediClient client, IStreamIO streamIO) : base(client, streamIO)
        {

        }

        public async override Task<ActionBase> Run()
        {
            List<Visit> visits = await GetVisits();
            if (visits != null)
            {
                Visit visit = InputManager.GetInput();

            }
        }

        private async Task<List<Visit>> GetVisits()
        {
            try
            {
                return await _client.GetVisits();
            }
            catch (RequestException e)
            {
                _streamIO.ErrorElement.Interact(e);
                return null;
            }
        }
    }
}

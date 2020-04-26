using System.Threading.Tasks;
using Client.HttpClients;
using Client.IO.Abstract;
using Common;

namespace Client.Actions
{
    public class MainMenuAction : ActionBase
    {
        public MainMenuAction(MediClient client, IStreamIO streamIO) : base(client, streamIO)
        {

        }

        public override Task<ActionBase> Run()
        {
            ActionBase nextAction;
            if (_client.User is Doctor)
            {
                nextAction = new DoctorMainMenuAction(_client, _streamIO);
            }
            else
            {
                nextAction = new PatientMainMenuAction(_client, _streamIO);
            }

            return Task.FromResult(nextAction);
        }
    }
}

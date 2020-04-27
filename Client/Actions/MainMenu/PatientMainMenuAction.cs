using System.Threading.Tasks;
using Client.Actions.IOManagers;
using Client.HttpClients;
using Client.IO.Abstract;

namespace Client.Actions
{
    public class PatientMainMenuAction : ActionBase
    {
        public InputManagerBase<ActionBase> InputManager { get; set; }

        public PatientMainMenuAction(MediClient client, IStreamIO streamIO) : base(client, streamIO)
        {
            InputManager = new PatientsMainMenuIO(client, streamIO);
        }

        public override Task<ActionBase> Run()
        {
            InputManager.PrintInstructions();
            ActionBase nextAction = InputManager.GetInput();
            return Task.FromResult(nextAction);
        }
    }
}

using System.Threading.Tasks;
using Client.Actions.InputManagers;
using Client.HttpClients;
using Client.IO.Abstract;

namespace Client.Actions
{
    public class DoctorMainMenuAction : ActionBase
    {
        public InputManagerBase<ActionBase> InputManager { get; set; }

        public DoctorMainMenuAction(MediClient client, IStreamIO streamIO) : base(client, streamIO)
        {
            InputManager = new DoctorMainMenuIO(client, streamIO);
        }

        public override Task<ActionBase> Run()
        {
            InputManager.PrintInstructions();
            ActionBase nextAction = InputManager.GetInput();
            return Task.FromResult(nextAction);
        }
    }
}

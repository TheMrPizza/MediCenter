using System.Threading.Tasks;
using Client.Actions.InputManagers;
using Client.HttpClients;
using Client.IO.Abstract;

namespace Client.Actions
{
    public class HomeMenuAction : ActionBase
    {
        public InputManagerBase<ActionBase> InputManager { get; set; }

        public HomeMenuAction(MediClient client, IStreamIO streamIO) : base(client, streamIO)
        {
            InputManager = new HomeMenuInput(client, streamIO);
        }

        public override Task<ActionBase> Run()
        {
            InputManager.PrintInstructions();
            ActionBase nextAction = InputManager.GetInput();
            return Task.FromResult(nextAction);
        }
    }
}

using System.Collections.Specialized;
using Client.HttpClients;
using Client.IO.Abstract;

namespace Client.Actions.InputManagers
{
    public class HomeMenuInput : InputManagerBase<ActionBase>
    {
        private OrderedDictionary _options;

        public HomeMenuInput(MediClient client, IStreamIO streamIO) : base(client, streamIO)
        {
            _options = new OrderedDictionary {
                { "Sign In", new SignInAction(client, streamIO) },
                { "Register", new RegisterAction(client, streamIO) } };
        }

        public override void PrintInstructions()
        {
            _streamIO.TextElement.Interact("Welcome to MediCenter!");
        }

        public override ActionBase GetInput()
        {
            return _streamIO.ListElement.Interact(_options) as ActionBase;
        }
    }
}

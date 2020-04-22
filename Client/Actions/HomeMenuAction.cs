using System.Collections.Specialized;
using System.Threading.Tasks;
using Client.HttpClients;
using Client.IO.Abstract;

namespace Client.Actions
{
    public class HomeMenuAction : ActionBase
    {
        private OrderedDictionary _options;

        public HomeMenuAction(MediClient client, IStreamIO streamIO) : base(client, streamIO)
        {
            _options = new OrderedDictionary {
                { "Sign In", new SignInAction(client, streamIO) },
                { "Register", new RegisterAction(client, streamIO) } };
        }

        public override Task<ActionBase> Run()
        {
            _streamIO.TextElement.Interact("Welcome to MediCenter!");
            ActionBase nextAction = _streamIO.ListElement.Interact(_options) as ActionBase;
            return Task.FromResult(nextAction);
        }
    }
}

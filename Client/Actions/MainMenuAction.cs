using System;
using System.Threading.Tasks;
using Client.HttpClients;
using Client.IO.Abstract;

namespace Client.Actions
{
    public class MainMenuAction : ActionBase
    {
        public MainMenuAction(MediClient client, IStreamIO streamIO) : base(client, streamIO)
        {

        }

        public override Task<ActionBase> Run()
        {
            _streamIO.TextElement.Interact("Hello " + _client.User.Name + "!");
            return Task.FromResult<ActionBase>(null);
        }
    }
}

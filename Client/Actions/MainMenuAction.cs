using System.Threading.Tasks;
using System.Collections.Specialized;
using Client.HttpClients;
using Client.IO.Abstract;

namespace Client.Actions
{
    public class MainMenuAction : ActionBase
    {
        private OrderedDictionary _options { get; set; }

        public MainMenuAction(MediClient client, IStreamIO streamIO) : base(client, streamIO)
        {
            _options = new OrderedDictionary {
                { "Order visit",  new OrderVisitAction(client, streamIO) } };
        }

        public override Task<ActionBase> Run()
        {
            _streamIO.TextElement.Interact("Hello " + _client.User.Name + "!");
            _streamIO.TextElement.Interact("What would you like to do?");
            ActionBase nextAction = _streamIO.ListElement.Interact(_options) as ActionBase;
            return Task.FromResult(nextAction);
        }
    }
}
